<template>
  <div>
    <h1>Demo Chart 1</h1>
    <svg :height="height" :width="width">
      <g transform="translate(50,50)">
        <circle
          v-for="c in output"
          :key="c.id"
          :r="c.r"
          :cx="c.x"
          :cy="c.y"
          :fill="c.fill"
          :stroke="c.stroke"
        ></circle>
      </g>
    </svg>
  </div>
</template>

<script>
import * as d3 from 'd3';

export default {
  name: 'chart1',
  props: ['chartData'],
  data() {
    return {
      height: 600,
      width: 600,
    };
  },
  created() {
    this.colourScale = d3
      .scaleOrdinal()
      .range(['#5EAFC6', '#FE9922', '#93c464', '#75739F']);
  },
  computed: {
    loadData() {
      const nestedTweets = d3.group(this.chartData, d => d.user);
      const packableTweets = { id: 'All Tweets', values: nestedTweets };
      return d3
        .hierarchy(packableTweets, (d) => d.values)
        .sum((d) =>
          d.retweets ? d.retweets.length + d.favorites.length + 1 : 1
        );
    },
    output() {
      return this.loadChart();
    },

  },
  methods: {
    loadChart() {
      const packChart = d3.pack();
      packChart.size([500, 500]);
      packChart.padding(10);
      const output = packChart(this.loadData).descendants();

      return output.map((d, i) => {
        const fill = this.colourScale(d.depth);
        return {
          id: i + 1,
          r: d.r,
          x: d.x,
          y: d.y,
          fill,
          stroke: 'gray',
        };
      });
    },
  },
};
</script>
