using System;
using System.Collections.Generic;
using System.Linq;

class TimeSpan {
    public static List<TimeSpan> Logs { get; private set; }

    // TimeSpan stats

    public decimal Open { get; private set; }
    public decimal Close { get; private set; }


    public decimal High { get; private set; }
    public decimal Low { get; private set; }

    public int Volume { get; private set; }


    public static void LogTransaction(decimal price, int volume) {
        TimeSpan current = Logs.LastOrDefault();

        // Log price
        if(price > current.High) {
            current.High = price;
        }
        if(price < current.Low) {
            current.Low = price;
        }
        // Log volume
        current.Volume += volume;
    }


    
    // constructor
    public TimeSpan(int open) {
        Open = open;
    }
}