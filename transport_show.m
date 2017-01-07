 q=uigetfile('best_pointsx.txt');
X=dlmread(q);
f=uigetfile('best_pointsy.txt');
% 
Y=dlmread(f);
f1= uigetfile('centroids.txt');
C=dlmread(f1);
figure
plot(X,Y)

hold on
plot(C(:,1),C(:,2),'*')
grid on