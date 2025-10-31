import { useEffect, useRef, useState } from "react";
import * as d3 from "d3";

export default function Graph() {
  
    const [rngArray, setRngArray] = useState([]);
    const [lastStr, setLastStr] = useState("");
    const svgRef = useRef(null);

    
    const maxItems = 50;   
    const timeOut = 100;  
    const maxValue = 1;   

   
    useEffect(() => {
        const id = setInterval(() => {
            const val = Math.random(); 
            const mock =
                `3/8 → 7/16: note:d4 s:supersaw ` +
                `cutoff:300 attack:0 decay:0 sustain:0.5 release:0.1 ` +
                `room:0.6 lpenv:3.3 gain:${val} duration:${Math.random()} ` +
                `background-color: black;color:white;border-radius:15px`;
            setLastStr(mock);
            setRngArray(prev => {
                const next = [...prev, mock];
                if (next.length > maxItems) next.shift();
                return next;
            });
        }, timeOut);
        return () => clearInterval(id);
    }, []);

  
    function logToNum(input) {
        if (!input) return 0;
        const parts = input.split(/\s+/);
        for (const p of parts) {
            if (p.startsWith("gain:")) {
                const v = Number(p.substring(5));
                return Number.isFinite(v) ? v : 0;
            }
        }
        return 0;
    }

    
    useEffect(() => {
        const svg = d3.select(svgRef.current);
        svg.selectAll("*").remove();
        if (rngArray.length === 0) return;

       
        const box = svg.node().getBoundingClientRect();
        const margin = { top: 10, right: 10, bottom: 20, left: 30 };
        const w = Math.max(100, box.width - margin.left - margin.right);
        const h = Math.max(100, box.height - margin.top - margin.bottom);

        const g = svg.append("g")
            .attr("transform", `translate(${margin.left},${margin.top})`);

        
        const data = rngArray.map(logToNum);

        const x = d3.scaleLinear().domain([0, data.length - 1]).range([0, w]);
        const y = d3.scaleLinear().domain([0, maxValue]).range([h, 0]);

      
        const defs = svg.append("defs");
        defs.append("linearGradient")
            .attr("id", "line-gradient")
            .attr("gradientUnits", "userSpaceOnUse")
            .attr("x1", 0).attr("x2", 0)
            .attr("y1", y(0)).attr("y2", y(maxValue))
            .selectAll("stop")
            .data([
                { offset: "0%", color: "green" },
                { offset: "100%", color: "red" }
            ])
            .enter()
            .append("stop")
            .attr("offset", d => d.offset)
            .attr("stop-color", d => d.color);

      
        const line = d3.line()
            .x((d, i) => x(i))
            .y(d => y(d));

        g.append("path")
            .datum(data)
            .attr("fill", "none")
            .attr("stroke", "url(#line-gradient)") 
           
            .attr("stroke-width", 1.5)
            .attr("d", line);

        g.append("g").call(d3.axisLeft(y).ticks(5)); 
    }, [rngArray]);

    return (
        <div className="App container">
            <h2>RNG Output: {lastStr}</h2>
            <svg
                ref={svgRef}
                width="100%"
                height="600px"
                className="border border-primary rounded p-2"
            />
        </div>
    );
}
