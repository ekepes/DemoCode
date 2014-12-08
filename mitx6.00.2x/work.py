import math
import random

def sim(numTrials, experiment):
    successEvents = 0.0
    for i in range(0, numTrials):
        if experiment():
            successEvents += 1

    return successEvents / numTrials

def singleSuccess():
    return random.randint(1, 10) == 1

def onlyOnce():
    successes = singleSuccess() + singleSuccess() + singleSuccess()
    return successes == 1

def atLeastOnce():
    successes = singleSuccess() + singleSuccess() + singleSuccess()
    return successes >= 1

def twoOrMore():
    successes = singleSuccess() + singleSuccess() + singleSuccess()
    return successes >= 2
