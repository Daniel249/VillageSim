using System;
using System.Collections.Generic;
using System.Linq;

class TimeSpan {

    // TimeSpan stats

    public decimal Open { get; private set; }
    public decimal Close { get; private set; }


    public decimal High { get; private set; }
    public decimal Low { get; private set; }

    public int Volume { get; private set; }


    public void LogTransaction(decimal price, int volume) {

        // Log open close
        if(Open == 0) {
            Open = price;
        }
        Close = price;
        // Log high low
        if(price > High) {
            High = price;
        }
        if(price < Low) {
            Low = price;
        }
        // Log volume
        Volume += volume;
    }


    // constructor
    public TimeSpan() {

    }
}