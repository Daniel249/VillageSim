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

    public int ResourceSupply { get; set; }
    public int ResourceDemand { get; set; }

    public void LogTransaction(decimal price, int resVolume, decimal curVolume) {

        // Log open close
        if(Open == 0) {
            Open = price;
            Low = price;
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

    // copy values from another TimeSpan
    public void CopyLog(TimeSpan timeSpan) {
        Open = timeSpan.Open;
        Close = timeSpan.Close;
        High = timeSpan.High;
        Low = timeSpan.Low;
        ResourceVolume = timeSpan.ResourceVolume;
        CurrencyVolume = timeSpan.CurrencyVolume;
    }


    // constructor
    public TimeSpan() {

    }
}