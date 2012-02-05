#ifndef _GeoObject_H_                   // include file only once
#define _GeoObject_H_


typedef struct color {
    float r, g, b;		// Color (R,G,B values)
} color;
typedef struct sphere {
    int x,y,z,r;
} sphere;
typedef struct vect{
    double x,y,z;
} Vect;
typedef struct ray{
    int x,y,z;
    Vect u;
} ray;
typedef struct position {
    int x,y,z;
} position;
class GeoObject{
public:


    virtual position intersect(ray r);
};

class Sphere: public GeoObject
{
protected:
    int x,y,z,r;
public:
    Sphere(int xin, int yin, int zin, int rin)
    {
        x = xin;
        y = yin;
        z = zin;
        r = rin;
    }
    position intersect(ray r)
    {
        position p = {0,0,0};
        return p;
    }
}

#endif
