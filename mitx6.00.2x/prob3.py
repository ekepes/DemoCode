import math

def stdDevOfLengths(L):
    """
    L: a list of strings

    returns: float, the standard deviation of the lengths of the strings,
      or NaN if L is empty.
    """

    N = len(L)

    if N == 0:
    	return float('NaN')

    total = 0

    for item in L:
    	total += len(item)

    mean = float(total) / N
    sumOfSquares = 0

    for item in L:
    	sumOfSquares += (len(item) - mean) ** 2

    return math.sqrt(sumOfSquares / N)
