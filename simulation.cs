using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

abstract class Simulation {

    // singleton reference
    public static Simulation SimInstance { get; protected set; }

    // run control
    public bool continueSim { get; set; }
    public bool nonStopTurns { get; set; }
    bool nextTurn = false;
    public void run() {
        SimSetUp();

        continueSim = true;
        nonStopTurns = false;

        while(continueSim) {
            if(Console.KeyAvailable) {
                processKey(Console.ReadKey(true));
            }
            if(nonStopTurns) {
                nextTurn = true;
            }
            // reset nextCycle before turn and sleep
            // so that it wont be overturned
            if(nextTurn) {
                nextTurn = false;
                // Console.WriteLine("turn");
                turn();
            } else {
                // System.Threading.Thread.Sleep(500);
                nextTurn = false;
                new System.Threading.ManualResetEvent(false).WaitOne(500);
            }
        }
    }

    // set up simulation
    protected abstract void SimSetUp();

    // main method on run()
    protected abstract void turn();
    

    // process key input
    void processKey(ConsoleKeyInfo key) {
        switch(key.Key) {
            // escape : close game
            case ConsoleKey.Escape:
                continueSim = false;
                break;
            // S : stop nontop
            case ConsoleKey.S:
                nonStopTurns = false;
                break;
            // A : start nonstop
            case ConsoleKey.A:
                nonStopTurns = true;
                break;
            // D : run one turn
            case ConsoleKey.D:
                nextTurn = true;
                break;
            case ConsoleKey.F:
                Interface.storeCurrentData();
                break;
            default:
                // not implemented
                break; 
        }
    }


    // constructor
    public Simulation() {
        
    }
}