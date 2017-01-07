import numpy as np
import io
from sklearn.cluster import KMeans
from datetime import datetime

with open('D:\\DataMiningProject\\transport\\pointsy.txt',  'r',encoding='utf-8' ) as f:
   
   X = np.array([(float(line.strip().split('\t')[0]),
                    float(line.strip().split('\t')[1])) for line in f])
                                                    
center = [3425.67079005, 3469.94198377]
kmeans = KMeans(n_clusters =37, max_iter=1000)
kmeans.fit(X)

centroids = kmeans.cluster_centers_
labels = kmeans.labels_
l = sorted(centroids, key = lambda x:x[0],reverse=True)
np.savetxt('C:\\Users\\Наташа\\Documents\\Matlab\\centroids.txt',l)
f1=open('D:\\DataMiningProject\\transport\\cent.txt','w')
#for li in centroids:
 #   print(li)
    


for li in l:
    print(li)
