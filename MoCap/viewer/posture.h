#ifndef _POSTURE_H
#define _POSTURE_H

#include "vector.h"
#include "types.h"

//Root position and all bone rotation angles (including root) 
class Posture
{
	//member functions
	public:
		friend Posture LinearInterpolate(float, Posture const&, Posture const& );

	//member variables
	public:

		//Root position (x, y, z)		
		vector root_pos;								
		
		//Rotation (x, y, z) of all bones at a particular time frame in their local coordinate system.
		//If a particular bone does not have a certain degree of freedom, 
		//the corresponding rotation is set to 0.
		//The order of the bones in the array corresponds to their ids in .ASf file: root, lhipjoint, lfemur, ...
		vector bone_rotation[MAX_BONES_IN_ASF_FILE];
		vector bone_translation[MAX_BONES_IN_ASF_FILE];
		vector bone_length[MAX_BONES_IN_ASF_FILE];
};

#endif