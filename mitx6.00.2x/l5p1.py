import math
import random

def noReplacementSimulation(numTrials):
    '''
    Runs numTrials trials of a Monte Carlo simulation
    of drawing 3 balls out of a bucket containing
    3 red and 3 green balls. Balls are not replaced once
    drawn. Returns the a decimal - the fraction of times 3 
    balls of the same color were drawn.
    '''
    allOneColorCount = 0.0
    for i in range(0, numTrials):
        draws = getBalls(3)
        if allOneColor(draws):
            allOneColorCount += 1

    return allOneColorCount / numTrials

def getBalls(numDraws):
    choices = ['red', 'red', 'red', 'green', 'green', 'green']
    draws = []
    for i in range(numDraws):
        draw = random.choice(choices)
        choices.remove(draw)
        draws.append(draw)

    return draws

def getCount(draws, color):
    count = 0
    for i in range(0, len(draws)):
        if draws[i] == color:
            count += 1

    return count

def allOneColor(draws):
    drawCount = len(draws)
    redCount = getCount(draws, 'red')
    if redCount == 0 or redCount == drawCount:
        return True
    return False
