
#include<fstream>
#include<math.h>
#include<iostream>
#include "GeoObject.h"
#include "glut.h"

/******************************************************************
Notes:
This is the same utility as in the earlier homework assignment.
Image size is 400 by 400 by default.  You may adjust this if
you want to.
You can assume the window will NOT be resized.
Call clearFramebuffer to clear the entire framebuffer.
Call setFramebuffer to set a pixel.  This should be the only
routine you use to set the color (other than clearing the
entire framebuffer).  drawit() will cause the current
framebuffer to be displayed.
As is, your ray tracer should probably be called from
within the display function.  There is a very short sample
of code there now.  You can replace that with a call to a
function that raytraces your scene (or whatever other
interaction you provide.
You may add code to any of the subroutines here,  You probably
want to leave the drawit, clearFramebuffer, and
setFramebuffer commands alone, though.
*****************************************************************/

#define ImageW 400
#define ImageH 400

float framebuffer[ImageH][ImageW][3];




// Draws the scene
void drawit(void)
{
    glDrawPixels(ImageW,ImageH,GL_RGB,GL_FLOAT,framebuffer);
    glFlush();
}

// Clears framebuffer to black
void clearFramebuffer()
{
    int i,j;

    for(i=0;i<ImageH;i++) {
        for (j=0;j<ImageW;j++) {
            framebuffer[i][j][0] = 0.0;
            framebuffer[i][j][1] = 0.0;
            framebuffer[i][j][2] = 0.0;
        }
    }
}

// Sets pixel x,y to the color RGB
void setFramebuffer(int x, int y, float R, float G, float B)
{
    if (R<=1.0)
        if (R>=0.0)
            framebuffer[x][y][0]=R;
        else
            framebuffer[x][y][0]=0.0;
    else
        framebuffer[x][y][0]=1.0;
    if (G<=1.0)
        if (G>=0.0)
            framebuffer[x][y][1]=G;
        else
            framebuffer[x][y][1]=0.0;
    else
        framebuffer[x][y][1]=1.0;
    if (B<=1.0)
        if (B>=0.0)
            framebuffer[x][y][2]=B;
        else
            framebuffer[x][y][2]=0.0;
    else
        framebuffer[x][y][2]=1.0;
}
double getLength(position a, position b)
{
    double result = sqrt(pow(a.x - b.x,2.0) + pow(a.y-b.y,2.0) + pow(a.z - b.z,2.0));
    return result;
}
ray createRay(position rp, position pixel)
{
    ray r;
    vect v;
    double u = getLength(rp,pixel);

    r.x = rp.x;
    r.y = rp.y;
    r.z = rp.z;

    v.x = (pixel.x - rp.x)/u;
    v.y = (pixel.y - rp.y)/u;
    v.z = (pixel.z - rp.z)/u;
    r.u = v;

    return r;
}

double dotProduct(vect a, vect b)
{
    return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
}

double vectLength(vect a)
{
    return sqrt(pow(a.x,2)+pow(a.y,2)+pow(a.z,2));
}
bool intersectSphere(ray R, sphere S)
{
    vect deltaP = {S.x - R.x,S.y-R.y, S.z-R.z};

    double part1 = pow(dotProduct(R.u, deltaP),2.0); 
    double part2 = pow(vectLength(deltaP),2.0);
    double part3 = pow(S.r,2.0);
       double result = part1 - part2 + part3;
    if(result<=0) return false;
    return true;
}

color rayTrace(ray R, int depth)
{
    sphere s = {200,200,25,100};
    color r = {1.0,0,0};

    color w = {1.0,1.0,1.0};
    bool b = intersectSphere(R,s);

    if(b) return r;
    return w;
}
void renderPicture()
{
    for(int x = 0; x<ImageW; x++)
    {
        for(int y =0; y<ImageH; y++)
        {
            std::cout<<x<<","<<y<<std::endl;
            position rp = {199,199,-200}; // reference point
            position pixel = {x,y,0};
            ray r = createRay(rp,pixel);
            color c = rayTrace(r, 0);
            setFramebuffer(x,y,c.r,c.g,c.b);
        }
    }
}


void display(void)
{
    renderPicture();

    drawit();
    std::cout<<"Drew picture"<<std::endl;
}

void init(void)
{
    clearFramebuffer();
}

int main(int argc, char** argv)
{
    glutInit(&argc,argv);
    glutInitDisplayMode(GLUT_SINGLE|GLUT_RGB);
    glutInitWindowSize(ImageW,ImageH);
    glutInitWindowPosition(100,100);
    glutCreateWindow("Andrew Johnson - Homework 5");
    init();	
    glutDisplayFunc(display);
    glutMainLoop();
    return 0;
}
