import numpy as np
import io
from sklearn.cluster import KMeans
from datetime import datetime

with open('D:\\DataMiningProject\\transport\\pointsy.txt',  'r',encoding='utf-8' ) as f:
   #f1=io.TextIOWrapper(f,'utf-8')
   X = np.array([(float(line.strip().split('\t')[0]),
                    float(line.strip().split('\t')[1])) for line in f])
                                                    
center = [3425.67079005, 3469.94198377]
kmeans = KMeans(n_clusters =37, max_iter=1000)
kmeans.fit(X)

centroids = kmeans.cluster_centers_
labels = kmeans.labels_
l = sorted(centroids, key = lambda x:x[0])
print(l)

