#pragma once
#include "mkl.h"

extern "C"  _declspec(dllexport)
bool mkl_func(MKL_INT nx, MKL_INT ny, float* x, float* y, int nlim, float* left, float* right, float* integrals, int& err);