import random
import pylab

# Global Variables
MAXRABBITPOP = 1000
CURRENTRABBITPOP = 500
CURRENTFOXPOP = 30

def rabbitGrowth():
    """ 
    rabbitGrowth is called once at the beginning of each time step.

    It makes use of the global variables: CURRENTRABBITPOP and MAXRABBITPOP.

    The global variable CURRENTRABBITPOP is modified by this procedure.

    For each rabbit, based on the probabilities in the problem set write-up, 
      a new rabbit may be born.
    Nothing is returned.
    """
    # you need this line for modifying global variables
    global CURRENTRABBITPOP

    for r in range(CURRENTRABBITPOP):
        rabbitChance = 1.0 - (float(CURRENTRABBITPOP) / float(MAXRABBITPOP))
        #print float(CURRENTRABBITPOP), float(MAXRABBITPOP), (float(CURRENTRABBITPOP) / float(MAXRABBITPOP)), rabbitChance
        if random.random() < rabbitChance:
            CURRENTRABBITPOP += 1
            
def foxGrowth():
    """ 
    foxGrowth is called once at the end of each time step.

    It makes use of the global variables: CURRENTFOXPOP and CURRENTRABBITPOP,
        and both may be modified by this procedure.

    Each fox, based on the probabilities in the problem statement, may eat 
      one rabbit (but only if there are more than 10 rabbits).

    If it eats a rabbit, then with a 1/3 prob it gives birth to a new fox.

    If it does not eat a rabbit, then with a 1/10 prob it dies.

    Nothing is returned.
    """
    # you need these lines for modifying global variables
    global CURRENTRABBITPOP
    global CURRENTFOXPOP

    for f in range(CURRENTFOXPOP):
        foxEatsChance = (float(CURRENTRABBITPOP) / float(MAXRABBITPOP))
        if CURRENTRABBITPOP > 10 and random.random() < foxEatsChance:
            CURRENTRABBITPOP -= 1
            if random.choice(['X', ' ', ' ']) == 'X':
                CURRENTFOXPOP += 1
        else:
            if CURRENTFOXPOP > 10 and random.random() < 0.1:
                CURRENTFOXPOP -= 1
    
            
def runSimulation(numSteps):
    """
    Runs the simulation for `numSteps` time steps.

    Returns a tuple of two lists: (rabbit_populations, fox_populations)
      where rabbit_populations is a record of the rabbit population at the 
      END of each time step, and fox_populations is a record of the fox population
      at the END of each time step.

    Both lists should be `numSteps` items long.
    """

    #stepResults = []
    stepRabbits = []
    stepFoxes = []
    for s in range(numSteps):
        rabbitGrowth()
        foxGrowth()
        #stepResults.append((CURRENTRABBITPOP, CURRENTFOXPOP))
        stepRabbits.append(CURRENTRABBITPOP)
        stepFoxes.append(CURRENTFOXPOP)

    #return stepResults
    return (stepRabbits, stepFoxes)
