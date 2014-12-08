def powerSet(items):
    N = len(items)
    # enumerate the 2**N possible combinations
    for i in xrange(2**N):
        combo = []
        for j in xrange(N):
            # test bit jth of integer i
            if (i >> j) % 2 == 1:
                combo.append(items[j])
        yield combo

def dToT(n, numDigits):
    """requires: n is a natural number less than 2**numDigits
      returns a binary string of length numDigits representing the
              the decimal number n."""
    assert type(n)==int and type(numDigits)==int and n >=0 and n < 3**numDigits
    bStr = ''
    while n > 0:
        bStr = str(n % 3) + bStr
        n = n//3
    while numDigits - len(bStr) > 0:
        bStr = '0' + bStr
    return bStr

def yieldAllCombos(items):
    """
      Generates all combinations of N items into two bags, whereby each 
      item is in one or zero bags.

      Yields a tuple, (bag1, bag2), where each bag is represented as 
      a list of which item(s) are in each bag.
    """
    N = len(items)
    templates = []
    numSubsets = 3**len(items)
    for i in range(numSubsets):
        templates.append(dToT(i, len(items)))

    # enumerate the 3**N possible combinations
    for t in templates:
        bagOne = []
        bagTwo = []
        for j in range(len(t)):
            if t[j] == '1':
                bagOne.append(items[j])
            if t[j] == '2':
                bagTwo.append(items[j])
        yield bagOne, bagTwo