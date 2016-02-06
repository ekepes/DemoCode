from thinkbayes import Suite
    
class Cookie(Suite):
    mixes = {
        'Bowl 1':dict(vanilla=0.75, chocolate=0.25),
        'Bowl 2':dict(vanilla=0.5, chocolate=0.5),
    }

    def Likelihood(self, data, hypo):
        mix = self.mixes[hypo]
        like = mix[data]
        return like

def main():
    suite = Cookie(['Bowl 1', 'Bowl 2'])
    suite.Update('vanilla')
    suite.Print()

if __name__ == '__main__':
    main()