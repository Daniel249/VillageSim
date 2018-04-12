using System;
using System.Collections.Generic;
using System.Linq;

class TimeSpan {

    // TimeSpan stats

    public decimal Open { get; private set; }
    public decimal Close { get; private set; }


    public decimal High { get; private set; }
    public decimal Low { get; private set; }

    public int ResourceVolume { get; private set; }
    public decimal CurrencyVolume { get; private set; }


    public void LogTransaction(decimal price, int resVolume, decimal curVolume) {

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
        ResourceVolume += resVolume;
        CurrencyVolume += curVolume;
    }


    // constructor
    public TimeSpan() {

    }
}