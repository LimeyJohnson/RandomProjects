#include "stdio.h"
#include <iostream>
using namespace std;
#include "glut.h"

static GLfloat spin = 0.0;
static int win;
static GLfloat cl = 0.0;
static GLfloat bgRed = 1.0, bgGreen=1.0, bgBlue=1.0 ;
static GLfloat spinSpeed = 5.0;
static int mousey=0;
static int mousex=0;
bool mousePressed = false, midpressed = false;
static GLfloat bgAlpha = 0.0;
static int screenWidth, screenHeight;
static GLenum type = GL_POLYGON;
static GLfloat xoff = 0.0;
static GLfloat yoff = 0.0;
void init(void)
{
    glClearColor(bgRed,bgGreen,bgBlue,bgAlpha);
    glShadeModel(GL_FLAT);
    gluOrtho2D(0.0,500.0,0.0,500.0);
}
void display(void)
{
    glClear(GL_COLOR_BUFFER_BIT);
    glPushMatrix();
    glRotatef(spin,0.0,0.0,1.0);
    glColor3f(bgAlpha,bgAlpha,bgAlpha);
    //glRectf(-25.0,-25.0,25.0,25.0);
    glBegin(type);

    //glVertex2f(118.0,147.0);
    //glVertex2f(174.0,76.0);
    //glVertex2f(209.0,131.0);
    //glVertex2f(281.0,167.0);
    //glVertex2f(218.0,203.0);
    glVertex2f(0.0+xoff,0.0-yoff);
    glVertex2f(0.0+xoff,3.0-yoff);
    glVertex2f(45+xoff,23-yoff);
    glVertex2f(6.0+xoff,-20.0-yoff);
    glVertex2f(4.0+xoff,0.0-yoff);
    glEnd();
    glPopMatrix();
    glutSwapBuffers();
}
void spinDisplay(void)
{
    spin = spin+spinSpeed;
    if(spin>360)spin = spin - 360;
    glutPostRedisplay();
}
void reshape(int w, int h){
    glViewport(0,0,(GLsizei)w, (GLsizei) h);
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    glOrtho(-50.0,50.0,-50.0,50.0,-1.0,1.0);
    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
}
void motion(int x, int y){
   // cout<<"Mouse motion ("<<((x+(screenWidth/2)-250.0)/screenWidth)<<","<<((y+250.5)/screenHeight)<<")";
    if(mousePressed){
        spinSpeed = (5.0*(screenHeight - 2*(y - 250))/screenHeight);

        bgAlpha = ((x+(screenWidth/2)-250.0)/screenWidth);
        cout<<"bgAlpha "<<bgAlpha<<endl;

    }
    if(midpressed){
        xoff = (x - mousex)/5;
        yoff = (y - mousey)/5;
    }
    glutPostRedisplay();
    
}
void mouse(int button, int state, int x, int y)
{

    if(button == GLUT_LEFT_BUTTON && state == GLUT_DOWN){
        mousePressed = true;
    }


    switch(button){
        case GLUT_LEFT_BUTTON:
            if(state == GLUT_DOWN){
                glutIdleFunc(spinDisplay);
                mousePressed = true;

                cout<<"Mouse pressed"<<endl;
            }
            if(state == GLUT_UP){
                cout<<"Mouse lifted"<<endl;
                glutIdleFunc(NULL);
                mousePressed = false;
                spinSpeed = 5.0;
            }
            break;

        case GLUT_MIDDLE_BUTTON:
            if(state == GLUT_DOWN){
                mousex = x;
                mousey = y;
                midpressed = true;
            }
            if(state == GLUT_UP){
                midpressed = false;
            }


            cout<<"middle button pressed"<<endl;
            break;

        case GLUT_RIGHT_BUTTON:
            if(state == GLUT_DOWN){
                spinSpeed = -5.0;
                glutIdleFunc(spinDisplay);

            }
            break;



        default:
            break;
    }
    glutPostRedisplay();
}
void keyboard(unsigned char key, int x, int y){
    cout<<"Pressed key "<< key<< " on corrdinates ("<<x<<","<<y<<")";
    cout<<endl;
    if(key =='q'){
        cout << "Got q, so quitting "<<endl;
        glutDestroyWindow(win);
    }
    if(key=='y'){
        cl=1.0;
    }
    if(key=='u'){
        cl=0.0;
    }
    if(key == '1'){
        type = GL_POINTS;
    }
    if(key == '2'){
        type = GL_LINE_LOOP;
    }
    if(key == '3'){
        type = GL_POLYGON;
    }
    if(key == 'r'){
        xoff = 0.0;
        yoff = 0.0;
    }
    if(key == 'c'){
       bgRed = 0.0;
       bgBlue = 1.0;
       bgGreen = 1.0;
    glClearColor(bgRed,bgGreen,bgBlue,bgAlpha);   
      
    }
     if(key == 'w'){
       bgRed = 1.0;
       bgBlue = 1.0;
       bgGreen = 1.0;
    glClearColor(bgRed,bgGreen,bgBlue,bgAlpha);   
      
    }
      if(key == 'y'){
       bgRed = 1.0;
       bgBlue = 0.0;
       bgGreen = 1.0;
    glClearColor(bgRed,bgGreen,bgBlue,bgAlpha);   
      
    }
       if(key == 'm'){
       bgRed = 1.0;
       bgBlue = 1.0;
       bgGreen = 0.0;
    glClearColor(bgRed,bgGreen,bgBlue,bgAlpha);   
      
    }
    
    glutPostRedisplay();
}
int main(int argc, char ** argv)
{

    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);
    glutInitWindowSize(500,500);
    
    screenHeight = glutGet(GLUT_SCREEN_HEIGHT);
    screenWidth = glutGet(GLUT_SCREEN_WIDTH);
    glutInitWindowPosition(((screenWidth/2)-250),((screenHeight/2)-250));
    cout<<screenHeight<<" "<<screenWidth<<endl;
    win = glutCreateWindow("Andrew Johnson - Assignment 2");
    init();
    glutDisplayFunc(display);
    glutReshapeFunc(reshape);
    glutMouseFunc(mouse);
    glutMotionFunc(motion);
    glutKeyboardFunc(keyboard);
    glutMainLoop();
    return 0;
}