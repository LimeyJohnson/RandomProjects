/* Andrew Johnson
    CPSC441 - Assignment 1
    Dr. Jinxiang Chi
    02/03/2009
*/
#include "glut.h"
void init(void)
{
    glClearColor(0.0,0.0,0.0,0.0);
    glMatrixMode(GL_PROJECTION);
    gluOrtho2D(0.0,200.0,0.0,150.0);
}
void lineSegment(void)
{
    //Starting Bottom Left going counter clockwise
    glClear(GL_COLOR_BUFFER_BIT);
    glColor3f(1.0,1.0,1.0);
    glBegin(GL_LINES);
        glVertex2i(75,11.7);
        glVertex2i(125,11.7);
    glEnd();
    glColor3f(0.0,1.0,0.0);
    glBegin(GL_LINES);
        glVertex2i(125,11.7);
        glVertex2i(150,75);
    glEnd();
    
    glColor3f(1.0,0.0,1.0);
    glBegin(GL_LINES);
        glVertex2i(150,75);
        glVertex2i(125,138.3);
    glEnd();

    glColor3f(1.0,0.5,0.0);
    glBegin(GL_LINES);
    glVertex2i(125,138.3);
    glVertex2i(75,138.3);
     glColor3f(0.25,0.0,0.0);   
    glEnd();
    glBegin(GL_LINES);
    glVertex2i(75,138.3);
    glVertex2i(50,75);
        
    glEnd();
    glColor3f(1.0,1.0,0.0);
    glBegin(GL_LINES);
    
    glVertex2i(50,75);
    glVertex2i(75,11.7);    
    glEnd();
    glFlush();
}
void main(int argc, char** argv)
{
    glutInit(&argc, argv);
    glutInitDisplayMode(GLUT_SINGLE|GLUT_RGB);
    glutInitWindowPosition(200,200);
    glutInitWindowSize(600,300);
    glutCreateWindow("Andrew Johnson - Assignment 1");

    init();
    glutDisplayFunc(lineSegment);
    glutMainLoop();
}
