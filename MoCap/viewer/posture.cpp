#include "posture.h"

/************************ Posture class functions **********************************/

//Output Posture  = (1-t)*a + t*b
Posture 
LinearInterpolate(float t, Posture const& a, Posture const& b )
{
	Posture InterpPosture;

	//Iterpolate root position
	InterpPosture.root_pos = interpolate(t, a.root_pos, b.root_pos);

	//Interpolate bones rotations
	for (int i = 0; i < MAX_BONES_IN_ASF_FILE; i++)
		InterpPosture.bone_rotation[i] = interpolate(t, a.bone_rotation[i], b.bone_rotation[i]);

	return InterpPosture;
}