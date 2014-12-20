def possible_mean(L):
    return sum(L)/len(L)

def possible_variance(L):
    mu = possible_mean(L)
    temp = 0
    for e in L:
        temp += (e-mu)**2
    return temp / len(L)

A = [0,1,2,3,4,5,6,7,8]
print 'A: ', possible_variance(A)

B = [5,10,10,10,15]
print 'B: ', possible_variance(B)

C = [0,1,2,4,6,8]
print 'C: ', possible_variance(C)

D = [6,7,11,12,13,15]
print 'D: ', possible_variance(D)

E = [9,0,0,3,3,3,6,6]
print 'E: ', possible_variance(E)