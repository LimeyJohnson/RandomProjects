


#include <math.h>
#include <vector>
#include <list>
#include <iostream>
#include "glut.h"

/******************************************************************
Notes:
Image size is 400 by 400 by default.  You may adjust this if
you want to.
You can assume the window will NOT be resized.
Call clearFramebuffer to clear the entire framebuffer.
Call setFramebuffer to set a pixel.  This should be the only
routine you use to set the color (other than clearing the
entire framebuffer).  drawit() will cause the current
framebuffer to be displayed.
As is, your scan conversion should probably be called from
within the display function.  There is a very short sample
of code there now.
You may add code to any of the subroutines here,  You probably
want to leave the drawit, clearFramebuffer, and
setFramebuffer commands alone, though.
*****************************************************************/

#define ImageW 400
#define ImageH 400


typedef struct position {
    int x,y;
} position;

typedef struct line {
    int tx, ty, bx, by;
    position start;
    position end;
} line;
typedef struct edge{
    int maxY;
    double currentX;
    double xIncr;
} edge;
float framebuffer[ImageH][ImageW][3];
bool isClipping = false;
int currentPoly = 0;
std::vector<position> polygons[10];
void motion(int x, int y);
typedef enum {Left, Right, Bottom, Top} Boundary;


typedef struct color {
    float r, g, b;		// Color (R,G,B values)
} color;
color colors[10]; // Different colors for polygons

int colorIndex;
bool mode; //true = drawing polygons, false = clipping
position initialPosition;
position currentPosition;
void setFramebuffer(int x, int y, float R, float G, float B);
// Draws the scene
void drawit(void)
{
    glDrawPixels(ImageW,ImageH,GL_RGB,GL_FLOAT,framebuffer);
    glFlush();
}
void ScanConversion(std::vector<edge> edges[]);
void clip(int ix, int iy, int ex, int ey);
void createEdgeTableAndScanConvert(std::vector<position> polygonIN)
{
    int x,y,z;
    std::vector<edge> edges[ImageH];
    std::vector<line> lines;
    lines.empty();
    //convert the positions to lines
    for (x = 0; x<polygonIN.size();x++){
        int tx, ty, bx, by, startx;
        position start = polygonIN.at(x);
        position end = polygonIN.at((x+1)%(polygonIN.size()));
        tx = start.x>end.x ? start.x : end.x;
        bx = start.x>end.x ? end.x : start.x;
        ty = start.y<end.y ? start.y :end.y;
        if(start.y<end.y){
            by = end.y;
            startx = end.x;
            position swap = start;
            start = end;
            end = swap;
        }
        else{
            by = start.y;
            startx = start.y;
        }

        line toBeAdded = {tx,ty,bx,by,start,end};
        lines.push_back(toBeAdded);
    }
    std::cout<<"number of edges: "<<lines.size()<<std::endl;
    for( y=ImageH;y>=0;y--)
    {
        std::vector<edge> edgeInsert;
        edgeInsert.empty();
        for each (line l in lines)
        {
            if(l.by == y)
            {
                int maxY, currentX;
                double xIncr;
                maxY = l.ty;
                currentX = l.start.x;
                xIncr = -1.0/( (double)(l.end.y - l.start.y) / (double) (l.end.x - l.start.x));
                edge temp = {maxY,currentX,xIncr};
                edges[y].push_back(temp);
                std::cout<<"inserting edge for min y val "<<l.by<<std::endl;
            }
        }

        if(edges[y].size()>0){
            //sort by currentX.. lowest first
            for(x = 0; x<edges[y].size()-1;x++)
            {
                for(z = 0; z<=edges[y].size()-2;z++)
                {
                    if(edges[y].at(z).currentX>edges[y].at(z+1).currentX){
                        edge temp = edges[y].at(z);
                        edges[y][z] = edges[y][z+1];
                        edges[y][z+1] = temp;
                    }
                }
            }
        }
    }
    ScanConversion(edges);
}
void ScanConversion(std::vector<edge> edges[]){
    std::list<edge> list;
    int y,x;
    for( y=ImageH-1;y>0;y--)
    {
        if(!edges[y].empty() || !list.empty())
        {	//Add edges to Active Edge List from Active Edge Table starting at line
            for each (edge e in edges[y]) list.push_back(e);
            //remove from list if they end at y
            while(list.front().maxY>y && !list.empty())list.pop_front();
            std::list<edge> tlist;
            //Draw line
            while(list.size()>1){
                edge a = list.front();
                list.pop_front();
                edge b = list.front();
                list.pop_front();
                if(a.currentX>b.currentX){
                    edge swapedge = a;
                    a = b;
                    b = swapedge;
                }
                for(x = a.currentX; x<b.currentX;x++)setFramebuffer(x,y,colors[currentPoly].r,colors[currentPoly].g,colors[currentPoly].b);
                a.currentX = a.currentX + a.xIncr;
                b.currentX = b.currentX + b.xIncr;
                if(a.maxY<(y-1))tlist.push_back(a);
                if(b.maxY<(y-1))tlist.push_back(b);
            }
            list.swap(tlist);
        }
        drawit();
    }
    for each (edge e in edges[y])
    {
        std::cout<<"edge currentX = "<<e.currentX<<" maxY = "<<e.maxY<<" xIncr = "<<e.xIncr<<std::endl;
    }
    
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
// I've made a small change to this function to make the pixels match
// those returned by the glutMouseFunc exactly - Scott Schaefer 
void setFramebuffer(int x, int y, float R, float G, float B)
{
    // changes the origin from the lower-left corner to the upper-left corner
    y = ImageH - 1 - y;
    if (R<=1.0)
        if (R>=0.0)
            framebuffer[y][x][0]=R;
        else
            framebuffer[y][x][0]=0.0;
    else
        framebuffer[y][x][0]=1.0;
    if (G<=1.0)
        if (G>=0.0)
            framebuffer[y][x][1]=G;
        else
            framebuffer[y][x][1]=0.0;
    else
        framebuffer[y][x][1]=1.0;
    if (B<=1.0)
        if (B>=0.0)
            framebuffer[y][x][2]=B;
        else
            framebuffer[y][x][2]=0.0;
    else
        framebuffer[y][x][2]=1.0;
}

void display(void)
{
    glClear(GL_COLOR_BUFFER_BIT);
    glPushMatrix();
    glColor3f(1.0,1.0,1.0);
    glBegin(GL_POLYGON);

    glVertex2f(initialPosition.x,initialPosition.y);
    glVertex2f(currentPosition.x,initialPosition.y);
    glVertex2f(currentPosition.x,currentPosition.y);
    glVertex2f(initialPosition.x,currentPosition.y);

    glEnd();
    glPopMatrix();
    glutSwapBuffers();
    glutPostRedisplay();
    drawit();
}

void mouse(int button, int state, int x, int y)
{
    if(mode){
        if(state == GLUT_DOWN) std::cout<<"x "<<x<<" y "<<y<<std::endl;
        switch(button){
            case GLUT_LEFT_BUTTON:
                if(state == GLUT_DOWN){
                    if(colorIndex<10){
                        if(polygons[currentPoly].size()<=9){
                            position temp = {x,y};
                            polygons[currentPoly].push_back(temp);
                            setFramebuffer(x,y,1.0,1.0,1.0);
                            drawit();
                        }
                        else std::cout<<"polygon can only have 10 sides, please right click to end polygon"<<std::endl;
                    }
                    else std::cout<<"Only 10 polygons allowed";
                }
                break;
            case GLUT_RIGHT_BUTTON:
                if(mode){
                    if(state == GLUT_DOWN && mode)
                    {
                        if(colorIndex<10){
                            std::cout<<"creating polygon"<<std::endl;
                            position temp = {x,y};
                            polygons[currentPoly].push_back(temp);
                            setFramebuffer(x,y,1.0,1.0,1.0);
                            createEdgeTableAndScanConvert(polygons[currentPoly]);
                            currentPoly++;
                            drawit();
                        }
                        else std::cout<<"Only 10 polygons allowed"<<std::endl;
                    }
                }
        }
        if(!mode){
            if(button == GLUT_RIGHT_BUTTON && state == GLUT_DOWN)
            {
                initialPosition.x=x;
                initialPosition.y=y;
                currentPosition.x = x;
                currentPosition.y = y;
                std::cout<<"Initial Position Set"<<std::endl;
                clearFramebuffer();
            }
        }



    }
}
void init(void)
{
    clearFramebuffer();
}

bool inside(position p, Boundary b, position wMin, position wMax)
{
    switch(b){
        case Left: if (p.x < wMin.x) return false; break;
        case Right: if(p.x > wMax.x) return false; break;
        case Bottom: if(p.y > wMax.y) return false; break;
        case Top: if (p.y < wMin.y) return false; break;
    }
    return true;
}
position intersect(position p1, position p2, Boundary winEdge, position wMin, position wMax)
{

    position iPt;
    GLfloat m;

    if(p1.x != p2.x) m = (p1.y = p2.y) / (p1.x  - p2.x);
    switch(winEdge){
    case Left:
        iPt.x = wMin.x;
        iPt.y = p2.y + (wMin.x - p2.x) *m;
        break;
    case Right:
        iPt.x = wMax.x;
        iPt.y = p2.y + (wMin.x - p2.x) *m;
        break;
    case Bottom:
        iPt.y = wMax.x;
        if (p1.x != p2.x) iPt.x = p2.x + (wMax.y - p2.y) / m;
        else iPt.x = p2.x;
        break;
    case Top:
        iPt.y = wMin.y;
        if (p1.x != p2.x) iPt.x = p2.x + (wMin.y - p2.y) / m;
        else iPt.x = p2.x;
        break;
    }
    return (iPt);
}
void keyboard(unsigned char key, int x, int y){
    if(key == 'c'){
        mode = false;
        
    }
    if(key = 'g'){
clearFramebuffer();
  //      motion (220,220);
    }
}
void motion(int x, int y){
    int a;
    std::cout<<"Current position = ("<<currentPosition.x<<","<<currentPosition.y<<") initial position = ("<<initialPosition.x<<","<<initialPosition.y<<")"<<std::endl;
    if(!mode)
    {
        for each (std::vector<position> polyIN in polygons){
            std::vector<position> final;
            std::vector<position> tempPoly = polyIN;
           /* currentPosition.x = 399;
            currentPosition.y = 399;
            */
            initialPosition.y = 190;
            initialPosition.x = 190;
            
            currentPosition.x = 220;
            currentPosition.y = 220;
            
            
            Boundary enums[] = {Left,Top,Right,Bottom};
            for(int b = 0; b<4; b++){
                 final.clear();
                for(a = 0; a<tempPoly.size();a++)
                {


                    position p1 = tempPoly.at(a);
                    position p2 = tempPoly.at((a+1)%tempPoly.size());

                    if(inside(p1,enums[b],initialPosition,currentPosition)){
                        if(inside(p2,enums[b],initialPosition,currentPosition)){
                            final.push_back(p2);
                        }
                        else final.push_back(intersect(p1,p2,enums[b],initialPosition,currentPosition));
                    }
                    else{ 
                        if(inside(p2,enums[b],initialPosition,currentPosition)){
                            final.push_back(p2);
                            final.push_back(intersect(p1,p2,enums[b],initialPosition,currentPosition));
                        }
                    }
                }
                tempPoly = final;
               
            }
            createEdgeTableAndScanConvert(final);


        }
    }

    drawit();
    glutPostRedisplay();
    std::cout<<"Mouse motion ("<<x<<","<<y<<")";
}





int main(int argc, char** argv)
{

    currentPosition.x = 0; currentPosition.y = 0;
    initialPosition.x = 0; initialPosition.y = 0;
    mode = true;
    color temp = {1.0,0.0,0.0};
    colors[0] = temp;
    colors[1].r = 0.0; colors[1].g = 1.0; colors[1].b = 0.0;
    colors[2].r = 0.0; colors[2].g = 0.0; colors[2].b = 1.0;
    colors[3].r = 1.0; colors[3].g = 1.0; colors[3].b = 0.0;
    colors[4].r = 0.0; colors[4].g = 1.0; colors[4].b = 1.0;
    colors[5].r = 1.0; colors[5].g = 1.0; colors[5].b = 1.0;
    colors[6].r = 1.0; colors[6].g = 0.0; colors[6].b = 1.0;
    colors[7].r = 0.5; colors[7].g = 0.5; colors[7].b = 0.5;
    colors[8].r = 0.75; colors[8].g = 0.75; colors[8].b = 0.75;
    colors[9].r = 0.1; colors[9].g = 0.1; colors[9].b = 0.1;
    colorIndex = 0;
    glutInit(&argc,argv);
    glutInitDisplayMode(GLUT_SINGLE|GLUT_RGB);
    glutInitWindowSize(ImageW,ImageH);
    glutInitWindowPosition(100,100);
    glutCreateWindow("Andrew Johnson - Homework 2");
    init();	
    glutMotionFunc(motion);
    glutKeyboardFunc(keyboard);
    glutDisplayFunc(display);
    glutMouseFunc(mouse);
    glutMainLoop();
    return 0;
}
