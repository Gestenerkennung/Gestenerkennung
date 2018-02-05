#!/usr/bin/env python2
# -*- coding: utf-8 -*-
"""
Created on Fri May  5 11:29:16 2017

@author: admin
"""

import os
from pathlib import Path
import numpy as np

# arr is organized as follows: [sampleIDX, frameIDX, histogramIDX]
# samples go from 0 to 80
arr2=np.zeros((1,80,625))


# iteration over classes
f='test/'+'test_vector.txt'
print (f)
with open(f) as f:
    j=0
    for line in f:
        st=line.split(" ")
        arr2[0,j,:]=(st[0:625])
        j=j+1
       
          
print ("Test Data saved")
np.save('test_data', arr2)
#a=np.array(data)
#a=np.reshape(a,(300*4,40,625))

#np.save('test_data', arr)
#b=np.load('test_data.npy')
#print b

#print (np.equal(arr,b))


"""
for i in range (0,80):
    for j in range (0,40):
        for k in range (0,625):
            print (a[i,j,k],"   -   ", arr[i,j,k])
            if (a[i,j,k]==arr[i,j,k]):
                continue
            else:
                print "NOT"
                break
            
            print "HI"
"""         


"""
if os.path.exists('/home/admin/rnn&lstm_gesture_recog/max_mins/mins/class1.npy')==True:
    overall_class = list(np.load('/home/admin/rnn&lstm_gesture_recog/max_mins/mins/class3.npy'))
print (np.array(overall_class)) 
""" 
    
