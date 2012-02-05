#ifdef WIN32
#include <FL/gl.h>
#include <GL/glu.h>
#endif

#include <cstdio>
#include <cstring>
#include <cmath>
#include <FL/glut.H>
#include <GL/gl.h>     
#include "skeleton.h"
#include "motion.h"
#include "display.h"
#include "transform.h"
#include "types.h"


Display::Display()
{
	m_SpotJoint = -1;
	numActors = 0;
	m_pActor[0] = NULL;
	m_pMotion[0] = NULL;
}

Display::~Display()
{
//   if(m_pActor != NULL) delete m_pActor;
//   if(m_pMotion != NULL) delete m_pMotion;
}


//Draws the world coordinate axis
static void draw_bone_axis() 
{
 
   glBegin(GL_LINES);
   // draw x axis in red, y axis in green, z axis in blue 
   glColor3f(1., .2, .2);
   glVertex3f(0., 0., 0.);
   glVertex3f(.5, 0., 0.);

   glColor3f(.2, 1., .2);
   glVertex3f(0., 0., 0.);
   glVertex3f(0., .5, 0.);

   glColor3f(.2, .2, 1.);
   glVertex3f(0., 0., 0.);
   glVertex3f(0., 0., .5);

   glEnd();
}

//Draws a line from point a to point b
static void draw_vector(float *a, float *b) 
{
   glBegin(GL_LINES);
   glColor3f(.5, .5, .5);
   glVertex3f(a[0], a[1], a[2]);
   glVertex3f(b[0], b[1], b[2]);
   glEnd();
}


//Pre-draw the bones using quadratic object drawing function
//and store them in the display list
void set_display_list(Bone *bone, GLuint *pBoneList)
{
   int j;
   GLUquadricObj *qobj;
   	int numbones = numBonesInSkel(bone[0]);
   *pBoneList = glGenLists(numbones);
   qobj=gluNewQuadric();

   gluQuadricDrawStyle(qobj, (GLenum) GLU_FILL);
   gluQuadricNormals(qobj, (GLenum) GLU_SMOOTH);
   for(j=0;j<numbones;j++)
   {
      glNewList(*pBoneList + j, GL_COMPILE);
      glScalef(bone[j].aspx, bone[j].aspy, 1.);
      gluSphere(qobj, bone[j].length/2.0, 20, 20);
      glEndList();
   }
}


/*

	Define M_k = Modelview matrix at the kth node (bone) in the heirarchy
	M_k stores the transformation matrix of the kth bone in world coordinates
	Our goal is to draw the (k+1)th bone, using its local information and M_k
	
	  In the k+1th node, compute the following matrices:
		rot_parent_current: this is the rotation matrix that 
							takes us from k+1 to the kth local coordinate system 
		R_k+1 : Rotation matrix for the k+1 th node (bone)
				using angles specified by the AMC file in local coordinates
		T_k+1 : Translation matrix for the k+1th node

	The update relation is given by:
		M_k+1 = M_k * (rot_parent_current) * R_k+1 + T_k+1
*/
void Display::drawBone(Bone *pBone,int skelNum)
{
	static float z_dir[3] = {0., 0., 1.};
	float r_axis[3], mag, theta;

	//Tranform (rotate) from the local coordinate system of this bone to it's parent
	//This step corresponds to doing: ModelviewMatrix = M_k * (rot_parent_current)
	glMultMatrixd((double*)&pBone->rot_parent_current);     

	//Draw the local coordinate system for the selected bone.
	if(pBone->idx == m_SpotJoint)
		draw_bone_axis();

	//rotate AMC 
	//This step corresponds to doing: ModelviewMatrix *= R_k+1
	if(pBone->doftz) glTranslatef(0.,0.,pBone->tz);
	if(pBone->dofty) glTranslatef(0.,pBone->ty,0.);
	if(pBone->doftx) glTranslatef(pBone->tx,0.,0.);
	
	
	if(pBone->dofz) glRotatef(pBone->drz, 0., 0., 1.);
	if(pBone->dofy) glRotatef(pBone->dry, 0., 1,  0.);
	if(pBone->dofx) glRotatef(pBone->drx, 1., 0., 0.);

	glColor3f(1., 1., 0.1);

	//Store the current ModelviewMatrix (before adding the translation part)
	glPushMatrix();

	//Compute tx, ty, tz - translation from pBone to it's child (in local coordinate system of pBone)
	float tx = pBone->dir[0]*pBone->length;
	float ty = pBone->dir[1]*pBone->length;
	float tz = pBone->dir[2]*pBone->length;


	// Use the current ModelviewMatrix to display the current bone
	// Rotate the bone from its canonical position (elongated sphere 
	// with its major axis parallel to X axis) to its correct orientation
	if(pBone->idx == root)
		glCallList(m_BoneList[skelNum] + pBone->idx);
	else
	{ 
		//translate to the center of the bone
		glTranslatef(tx/2.0, ty/2.0, tz/2.0);

		//Compute the angle between the canonical pose and the correct orientation 
		//(specified in pBone->dir) using cross product.
		//Using the formula: r_axis = z_dir x pBone->dir
		
		v3_cross(z_dir, pBone->dir, r_axis);

		theta =  GetAngle(z_dir, pBone->dir, r_axis);
		
		glRotatef(theta*180./M_PI, r_axis[0], r_axis[1], r_axis[2]);;
		glCallList(m_BoneList[skelNum] + pBone->idx);
	}

	glPopMatrix(); 

	// Finally, add the translation component to the ModelviewMatrix
	// This step corresponds to doing: M_k+1 = ModelviewMatrix += T_k+1
	glTranslatef(tx, ty, tz);

}

//Traverse the hierarchy starting from the root 
//Every node in the data structure has just one child pointer. 
//If there are more than one children for any node, they are stored as sibling pointers
//The algorithm draws the current node (bone), visits its child and then visits siblings
void Display::traverse(Bone *ptr,int skelNum)
{
   if(ptr != NULL)
   {
      glPushMatrix();
      drawBone(ptr,skelNum);
      traverse(ptr->child,skelNum);
      glPopMatrix();
      traverse(ptr->sibling,skelNum);
   }
}

//Draw the skeleton
void Display::show()
{
	        static int showed = 0;
        if (showed == 0){
				for (int i = 0; i < numActors; i++)
					set_display_list(m_pActor[i]->getRoot(), &m_BoneList[i]);
                showed = 1;
        }

   glPushMatrix();

   //Translate the root to the correct position (it is (0,0,0) if no motion is loaded)
//   glTranslatef(m_pActor->m_RootPos[0], m_pActor->m_RootPos[1], m_pActor->m_RootPos[2]);

   //draw the skeleton starting from the root
   for (int i = 0; i < numActors; i++)
   {
		glPushMatrix();
		glTranslatef(MOCAP_SCALE*m_pActor[i]->tx,MOCAP_SCALE*m_pActor[i]->ty,MOCAP_SCALE*m_pActor[i]->tz);
		glRotatef(m_pActor[i]->rx,1,0,0);
		glRotatef(m_pActor[i]->ry,0,1,0);
		glRotatef(m_pActor[i]->rz,0,0,1);
	   traverse(m_pActor[i]->getRoot(),i);
   
		glPopMatrix();
   }
   glPopMatrix();
}

void Display::loadMotion(Motion *pMotion)
{
	if (numActors - 1 > MAX_SKELS) return;
//set a pointer to the new motion

	if(m_pMotion[numActors-1]!=NULL) 
		delete m_pMotion[numActors-1];
	m_pMotion[numActors-1] = pMotion;
}

//Set actor for display
void Display::loadActor(Skeleton *pActor)
{
	//set a pointer to the actor info
	if (numActors > MAX_SKELS) return;

	m_pActor[numActors++] = pActor;

	//Create the display list for the skeleton.
	//All the bones are the elongated spheres centered at (0,0,0).
	//The axis of elongation is the X axis.
	set_display_list(m_pActor[numActors-1]->getRoot(), &m_BoneList[numActors-1]);
}



