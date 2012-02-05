/*
      motion.h
 
	  Motion class  

	  1. read an AMC file and store it in a sequence of state vector 
	  2. write an AMC file
	  3. export to a mrdplot format for plotting the trajectories
   
      You can add more motion data processing functions in this class. 

      Revision 1 - Steve Lin, Jan. 14, 2002
 	  Revision 2 - Alla and Kiran, Jan 18, 2002
*/

#ifndef _MOTION_H
#define _MOTION_H

#include "vector.h"
#include "types.h"
#include "posture.h"
#include "skeleton.h"

class Motion 
{
	//member functions 
    public:
		//Include Actor (skeleton) ptr
		Motion(char *amc_filename, float scale,Skeleton * pActor);
		//Use to creating motion from AMC file
		Motion(char *amc_filename, float scale);
		//Use to create default motion with specified number of frames
		Motion(int nFrameNum);
		//delete motion
       ~Motion();

       // scale is a parameter to adjust the translational parameter
       // This value should be consistent with the scale parameter used in Skeleton()
       // The default value is 0.06
       int readAMCfile(char* name, float scale);
       int writeAMCfile(char* name, float scale);

	   //Set all postures to default posture
	   //Root position at (0,0,0), orientation of each bone to (0,0,0)
	   void SetPosturesToDefault();

	   //Set posture at spesified frame
	   void SetPosture(int nFrameNum, Posture InPosture);
		int GetPostureNum(int nFrameNum);
		void SetTimeOffset(int n_offset);
	   Posture* GetPosture(int nFrameNum);
	   void SetBoneRotation(int nFrameNum, vector vRot, int nBone);
	   void SetRootPos(int nFrameNum, vector vPos);

	//data members
	public:
       int m_NumFrames; //Number of frames in the motion 
	   int offset;

//	   int m_NumDOFs;	//Overall number of degrees of freedom (summation of degrees of freedom for all bones)
		Skeleton * pActor;
	   //Root position and all bone rotation angles for each frame (as read from AMC file)
	   Posture* m_pPostures; 
};

#endif
