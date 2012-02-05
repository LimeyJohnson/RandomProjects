/*
	skeleton.h

	Definition of the skeleton. 

    Written by  Jehee Lee
*/

#ifndef _VECTOR_H
#define _VECTOR_H


class vector
{
    // negation
    friend vector    operator-( vector const& );

    // addtion
    friend vector    operator+( vector const&, vector const& );

    // subtraction
    friend vector    operator-( vector const&, vector const& );

    // dot product
    friend float    operator%( vector const&, vector const& );

    // cross product
    friend vector    operator*( vector const&, vector const& );

    // scalar Multiplication
    friend vector    operator*( vector const&, float );

    // scalar Division
    friend vector    operator/( vector const&, float );


    friend float    len( vector const& );
    friend vector	normalize( vector const& );

	friend vector       interpolate( float, vector const&, vector const& );

    friend float       angle( vector const&, vector const& );

  // member functions
  public:
    // constructors
    vector() {}
    vector( float x, float y, float z ) { p[0]=x; p[1]=y; p[2]=z; }
    vector( float a[3] ) { p[0]=a[0]; p[1]=a[1]; p[2]=a[2]; }
	~vector() {};

    // inquiry functions
    float& operator[](int i) { return p[i];}

    float x() const { return p[0]; };
    float y() const { return p[1]; };
    float z() const { return p[2]; };
    void   getValue( float d[3] ) { d[0]=p[0]; d[1]=p[1]; d[2]=p[2]; }
    void   setValue( float d[3] ) { p[0]=d[0]; p[1]=d[1]; p[2]=d[2]; }

	float getValue( int n ) const { return p[n]; }
	vector setValue( float x, float y, float z )
								   { p[0]=x, p[1]=y, p[2]=z; return *this; }
	float setValue( int n, float x )
								   { return p[n]=x; }

	float length() const;

    // change functions
    void set_x( float x ) { p[0]=x; };
    void set_y( float x ) { p[1]=x; };
    void set_z( float x ) { p[2]=x; };

	//data members
    float p[3]; //X, Y, Z components of the vector
};


#endif
