#!/usr/bin/env python2
# -*- coding: utf-8 -*-
"""
Created on Fri May  5 11:29:16 2017

@author: admin
"""

import os
from pathlib import Path
import numpy as np


data=[]
# arr is organized as follows: [sampleIDX, frameIDX, histogramIDX]
# samples go from 0 to 80
arr=np.zeros((40*10,80,625))
arr2=np.zeros((1,80,625))
path='data/'
f_n=1


# iteration over classes
for i in range(0,4):
        counter=0
        # iteration over samples
        while((counter<100)):
            f=path+'lcc_p1_k'+str(i)+'_g'+str(counter)+'.txt'
            if Path(f).exists() == False:
                f='test/'+'test_vector.txt'
                print (f)
                counter+=1
                with open(f) as f:
                    j = 0
                    for line in f:
                        st=line.split(" ")
                        arr2[0,j,:]=(st[0:625])
                        j+=1
                continue
            # iteration over frames
            j=0
            with open(f) as f:
                   
                for line in f:
                   st=line.split(" ")
                   arr[f_n-1,j,:]=(st[0:625])
                   j=j+1
                counter+=1
                f_n+=1
                
          
print ("Train")
np.save('train_data', arr)
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
    
