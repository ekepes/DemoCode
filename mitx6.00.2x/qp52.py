import random
import pylab

def generateScores(numTrials):
    """
    Runs numTrials trials of score-generation for each of
    three exams (Midterm 1, Midterm 2, and Final Exam).
    Generates uniformly distributed scores for each of 
    the three exams, then calculates the final score and
    appends it to a list of scores.
    
    Returns: A list of numTrials scores.
    """
    scoreList = []

    for i in range(numTrials):
        m1 = random.randint(50, 80)
        m2 = random.randint(60, 90)
        f = random.randint(55, 95)
        total = (0.25 * m1) + (0.25 * m2) + (0.50 * f)
        scoreList.append(total)

    return scoreList

def plotQuizzes():
    trials = 10000
    scoreList = generateScores(trials)

    pylab.figure(1)
    pylab.hist(scoreList, bins = 7)
    pylab.title("Distribution of Scores")
    pylab.xlabel("Final Score")
    pylab.ylabel("Number of Trials")
    pylab.show()

plotQuizzes()