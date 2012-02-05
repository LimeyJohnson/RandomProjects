/*
    interpolate.h

	Create interpolated motion.
 
    Revision 1 - Alla and Kiran, Jan 18, 2002
*/


#ifndef _INTERPOLATE_H
#define _INTERPOLATE_H


enum InterpType
{
	LINEAR = 0
};

enum AngleRepresent
{
	EULER = 0, QUATERNIAN
};


class Interpolator
{
	//member functions
	public: 
		//constructors, destructors
		Interpolator(Motion* pInitialMotion, char* pOffsetFileName);
		~Interpolator();
		
		//Set interpolation type
		void SetInterpType(InterpType InterpTypeToUse) {m_InterpTypeToUse = InterpTypeToUse;};

		//Set angle representation for interpolation
		void SetAngleRepres(AngleRepresent AngleRepresToUse) {m_AngleRepresToUse = AngleRepresToUse;};

		//Get error type or error string
		ErrorType GetErrorType(){return m_ErrorType;};
		void GetErrorString(char* pErrorStr);

		//Init m_pTimeDistArray array from pOffsetFileName file
		void ReadOffsetFile(char* pOffsetFileName);

		//Create interpolated motion and store it into 
		void Interpolate(Motion*& pInterpMotion);

	private:
		//Linear interpolation using euler angles
		void LinearInterpEulerAngles(Motion* pInterpMotion);


	//member variables
	private:
		InterpType m_InterpTypeToUse;		//Interpolation type (linear, b_splines, ....)
		AngleRepresent m_AngleRepresToUse;  //Angle representation (euler angles and quaternians)

		Motion* m_pSampledMotion;			//Motion to be interpolated
		int*	m_pTimeDistArray;			//Array containing information on the time distance (number of frames) 
											//that was skipped between two samples in the sampled motion (m_pSampledMotion)
											//(read from somename_offset.txt file)

		ErrorType m_ErrorType;				//Initially set to no error. If error occurs this will be set accordingly.
};

#endif
