#include "pch.h"
#include <time.h>
#include <stdio.h>
#include "mkl.h"
#include "mkl_df_types.h"


extern "C"  _declspec(dllexport)
bool mkl_func(MKL_INT nx, MKL_INT ny, float* x, float* y, int nlim, float* left, float* right, float* integrals, int& err)
{
	//nx - число узлов, ny - размерность функции, x - края [0, right], y - значения функции уложенные row-major
	DFTaskPtr my_task;

	//for (int i = 0; i < ny * nx; i++)
	//	printf("%.1f ", y[i]);
	//printf("\n");

	int res = dfsNewTask1D(&my_task, nx, x, DF_UNIFORM_PARTITION, ny, y, DF_MATRIX_STORAGE_ROWS);
	if (res != DF_STATUS_OK) {
		err = res;
		return false;
	}
	float* coef = new float[ny * 4 * (nx-1)];
	res = dfsEditPPSpline1D(my_task, DF_PP_CUBIC, DF_PP_NATURAL, DF_BC_FREE_END, NULL,
		DF_NO_IC, NULL, coef, DF_NO_HINT);
	if (res != DF_STATUS_OK) {
		err = res;
		return false;
	}
	//for (int i = 0; i < ny * 4 * (nx - 1); i++)
	//	printf("%.1f ", coef[i]);
	//printf("\n");

	res = dfsConstruct1D(my_task, DF_PP_SPLINE, DF_METHOD_STD);
	if (res != DF_STATUS_OK) {
		err = res;
		return false;
	}

	float* derivations = new float[ny * nx];
	res = dfsInterpolate1D(my_task, DF_INTERP, DF_METHOD_PP, nx, x,
		DF_UNIFORM_PARTITION, 1, new int[1]{ 1 }, NULL, derivations, DF_MATRIX_STORAGE_ROWS, NULL);
	if (res != DF_STATUS_OK) {
		err = res;
		return false;
	}
	//for (int i = 0; i < ny*nx; i++)
	//	printf("%.1f ", derivations[i]);
	//printf("\n");

	res = dfsIntegrate1D(my_task, DF_METHOD_PP, nlim, left, DF_UNIFORM_PARTITION, right, DF_UNIFORM_PARTITION,
		NULL, NULL, integrals, DF_MATRIX_STORAGE_ROWS);
	if (res != DF_STATUS_OK) {
		err = res;
		return false;
	}
	//for (int i = 0; i < nlim * ny; i++)
	//	printf("%f ", integrals[i]);

	dfDeleteTask(&my_task);
	return true;
}

