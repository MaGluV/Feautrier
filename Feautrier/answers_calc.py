#!/usr/bin/env python3
# -*- coding: utf-8 -*-

import numpy as np
import scipy as sc

if __name__ == '__main__':
	
	v1 = np.array([1.2,-5.4,0.1,9.8])
	v2 = np.array([3.5,4.4,3.6,-1.02])
	md = np.array([
				   [3.81,0,0,0],
				   [0,1.74,0,0],
				   [0,0,-10.22,0],
				   [0,0,0,0.98]
				   ])
	m1 = np.array([
					[1.62,7.34,-9.91,13.6],
					[8.19,-5.23,4.67,0.82],
					[20.66,-3.21,15.84,0.17],
					[6.54,3.21,1.02,-4.38]
					])
	m2 = np.array([
					[-6.08,17.24,3.46,-5.24],
					[-5.55,4.01,0.29,11.87],
					[1.22,7.32,-0.66,-4.0],
					[7.77,2.91,2.74,5.33]
					])
	
	print(v1 + v2)
	print(v1 - v2)
	print(np.matmul(v1,v2))
	print(np.matmul(m1,v2))
	print(m1 + m2)
	print(m1 - m2)
	print(np.matmul(m1,m2))
	print(np.linalg.inv(md))
	print(np.linalg.inv(m1))
