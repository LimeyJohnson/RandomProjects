/*
    video_texture.h

	Create video texture.
 
    Revision 1 - Alla 
*/

#ifndef _VIDEO_TEXTURE_H
#define _VIDEO_TEXTURE_H



class VideoTexture
{
	//member functions
	public: 
		//constructors, destructors
		VideoTexture(char* pFileNameSuffix, int nFileNum);
		~VideoTexture();

		//Get error type or error string
		ErrorType GetErrorType(){return m_ErrorType;};
		void GetErrorString(char* pErrorStr);

	//member variables
	private:
		Motion** m_pMotionArray;
		int m_nNumMotions;
		ErrorType m_ErrorType;		//Initially set to no error. If error occurs this will be set accordingly.

			
};

#endif