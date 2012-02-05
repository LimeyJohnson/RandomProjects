
#include <cstdlib>
#include <cstdio>
#include <cstring>
#include <iostream>
#include "motion.h"
#include "interpolator.h"
#include "types.h"




Interpolator::Interpolator(Motion* pSampledMotion, char* pOffsetFileName)
{
	//Set default interpolation type
	m_InterpTypeToUse = LINEAR;

	//set default angle representation to use for interpolation
	m_AngleRepresToUse = EULER;

	//Set motion to be interpolated
	m_pSampledMotion = pSampledMotion;

	//Set ErrorType to NO_ERROR
	m_ErrorType = NO_ERROR_SET;

	//Init m_pTimeDistArray array
	m_pTimeDistArray = NULL;
	ReadOffsetFile(pOffsetFileName);


}


Interpolator::~Interpolator()
{
	if (m_pTimeDistArray != NULL)
		delete m_pTimeDistArray;
}



/* 
	This function read the offset file.
	Input:
		char* pOffsetFileName - file to read

	Input file format:
		One integer per line. Each integer represets the time frame 
		in the original motion for each sample in the sampled motion (m_pSampledMotion).
		
		Example:
		1		(Firtst sample in m_pSampledMotion corresponds to frame 1 in the original motion)
		13		(Second sample in m_pSampledMotion corresponds to frame 13 in the original motion)
		22		(Third  sample in m_pSampledMotion corresponds to frame 22 in the original motion)
		....    (And so on. Number of lines in the file is equal to the number of frames in sampled file)
*/
void Interpolator::ReadOffsetFile(char* pOffsetFileName)
{
	//open the file
	FILE* pInFile = fopen(pOffsetFileName, "r");
	if (pInFile == NULL)
	{
		m_ErrorType = BAD_OFFSET_FILE;
		return;
	}

	//Allocate memory for m_pTimeDistArray
	m_pTimeDistArray = new int [m_pSampledMotion->m_NumFrames];

	int frameNum, frameNumPrev = 0;
	//read the file
	for (int i = 0; i < m_pSampledMotion->m_NumFrames; i++)
	{
		//read next line
		if (fscanf(pInFile, "%d", &frameNum) == -1)
		{
			m_ErrorType = BAD_OFFSET_FILE;
			return;
		}

		//Compute offset and store into array
		//offset = number of frames skipped between frame i and i-1
		m_pTimeDistArray[i] = frameNum - frameNumPrev - 1;	
		frameNumPrev = frameNum; 

	}
}

//Create interpolated motion
void Interpolator::Interpolate(Motion*& pInterpMotion) 
{
	//Do nothing if error is set
	if (m_ErrorType != NO_ERROR_SET)
	{
		pInterpMotion = NULL;
		return;
	}

	//Compute number of frames int the new (interpolated) motion 
	int nNumFrames = 0;
	
	//Count all skipped frames
	for (int i = 0; i < m_pSampledMotion->m_NumFrames; i++)
		nNumFrames += m_pTimeDistArray[i];
	
	//Add number of frames on the sampled file
	nNumFrames += m_pSampledMotion->m_NumFrames;

	//Allocate new motion - initially set to default motion
	pInterpMotion = new Motion(nNumFrames); 
	std::cout<<"in interpolator"<<std::endl;
	//Perform the interpolation
	if (m_InterpTypeToUse == LINEAR && m_AngleRepresToUse == EULER)
		LinearInterpEulerAngles(pInterpMotion);
	else
	{
		//For now only linear interpolation of euler angles is supported
		m_ErrorType = NOT_SUPPORTED_INTERP_TYPE;
		delete pInterpMotion;
		pInterpMotion = NULL;
		return;
	}
}


void Interpolator::LinearInterpEulerAngles(Motion* pInterpMotion)
{
	//Assume that the first frame of the sampled motion is equal to the
	//first frame of the original motion 
	//and thus equal to the first frame of interpolated motion
	pInterpMotion->SetPosture(0, m_pSampledMotion->m_pPostures[0]);

	int nCurPostureIndx = 1;
	//Fill in other postures
	for (int i = 1; i < m_pSampledMotion->m_NumFrames; i++)
	{
		//Fill in all skipped frames. 
		//Compute them using linear interpolation between frames i and i-1 in sampled motion
		float fInterpDist = 1.0/(m_pTimeDistArray[i] + 1.0);
		for (int j = 1; j <= m_pTimeDistArray[i]; j++)
		{
			float fTemp = fInterpDist*j;
			Posture InterPost = LinearInterpolate(fInterpDist*j, m_pSampledMotion->m_pPostures[i-1], 
																 m_pSampledMotion->m_pPostures[i]);
			
			pInterpMotion->SetPosture(nCurPostureIndx, InterPost);
			nCurPostureIndx++;
	
		}

		//Set not skipped frame (from sampled motion)
		pInterpMotion->SetPosture(nCurPostureIndx, m_pSampledMotion->m_pPostures[i]);
		nCurPostureIndx++;
	}
}


//Return error description
void Interpolator::GetErrorString(char* pErrorStr)
{
	switch (m_ErrorType)
	{
	
	case NO_ERROR_SET:
	default:
		strcpy(pErrorStr, "No Error.\n");
		break;

	case BAD_OFFSET_FILE:
		strcpy(pErrorStr, "Bad offset file.\n");
		break;

	case NOT_SUPPORTED_INTERP_TYPE:
		strcpy(pErrorStr, "This interpolation type is not supported.\n");
		break;

	}
}
