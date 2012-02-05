#include <stdio.h>
#include <string.h>
#include "pic.h"

/*
 * pic_alloc: allocate picture memory.
 * If opic!=0, then memory from opic->pix is reused (after checking that
 *    size is sufficient), else a new pixel array is allocated.
 * Great caution should be used when freeing, if pix memory is reused!
 */
Pic *pic_alloc(int nx, int ny, int bytes_per_pixel, Pic *opic) {
    Pic *p;
    int size = ny*nx*bytes_per_pixel;

    ALLOC(p, Pic, 1);
    p->nx = nx;
    p->ny = ny;
    p->bpp = bytes_per_pixel;
    if (opic && opic->nx*opic->ny*opic->bpp >= p->nx*p->ny*p->bpp) {
	p->pix = opic->pix;
	/* now opic and p have a common pix array */
    }
    else
	ALLOC(p->pix, Pixel1, size);
    return p;
}

void pic_free(Pic *p) {
    free(p->pix);
    free(p);
}



