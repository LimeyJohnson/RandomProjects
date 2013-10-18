$(document).ready(function () {

    function drawTreeMap(startNode) {

        var margin = { top: 40, right: 10, bottom: 10, left: 10 },
         width = 960 - margin.left - margin.right,
         height = 500 - margin.top - margin.bottom;
        var rootLevel = "root";
        var color = d3.scale.category20c();

        var treemap = d3.layout.treemap()
            .size([width, height])
            .sticky(true)
            .value(calcNodeCost)
            .children(function (d) {
                if (d.name && d.name === startNode.name) {
                    return d.children;
                }
                else {
                    return null;
                }
            });
        function calcNodeCost(node) {
            if (node.children) {
                var total = yearDiff(node.dob) || 0;
                $.each(node.children, function (index, value) {
                    total += calcNodeCost(value);
                });
                return total;
            }
            else {
                return yearDiff(node.dob);
            }
        }
        function yearDiff(date) {
            if (!date) return 0;
            var now = new Date();
            var bday = new Date(date);
            var age = now.getFullYear() - bday.getFullYear();
            if ((now.getMonth() == bday.getMonth() && now.getDate() < bday.getDate()) || now.getMonth() < bday.getMonth()) {
                age--;
            }
            return age;

        }
        var div = d3.select("body").append("div")
            .style("position", "relative")
            .style("width", (width + margin.left + margin.right) + "px")
            .style("height", (height + margin.top + margin.bottom) + "px")
            .style("left", margin.left + "px")
            .style("top", margin.top + "px");


        var node = div.datum(startNode).selectAll(".node")
            .data(treemap.nodes)
            .enter().append("div")
            .attr("class", "node")
            .call(position)
            .style("background", function (d) { return color(d.name); })
            .text(function (d) {
                return d.name + " (" + d.value + ")";
            });

        node.on("click", function (node) {
            if (node.children) {
                div.remove();
                $("#breadcrumbs").empty();
                drawTreeMap(node);
            }
        });
        var bcnode = startNode;
        var fullHtml;
        //update breadcrums
        while (bcnode.name && bcnode.name !== "root") {
            var ID = fixID(bcnode.parent.name);
            $("#breadcrumbs").prepend("<a class='bc" + ID + "' href='javascript:void(0)' >" + bcnode.parent.name + "</a> >");
            $(".bc" + ID).on("click",function () {
                    div.remove();
                    followBreadCrumb(treeData, $(this).attr("class").replace("bc",""));
                });
            bcnode = bcnode.parent;
        }
    }
    function fixID(varID) {
        var regSearch = /\s/gi;
        return varID.replace(regSearch, "").replace("&", "");
    }
    function followBreadCrumb(node, ID) {
        if (fixID(node.name) == ID) {
            $("#breadcrumbs").empty();
            drawTreeMap(node);
           
            return false;
        }
        else if (node.children) {
            $.each(node.children, function (index, item) {
                return followBreadCrumb(item, ID);
            });
        }
    }
    var treeData = {
        name: "root", children:
            [
                    {
                        name: "Henry & Lorraine", children:
                          [
                              {
                                  name: "Dad", dob: "7/3/1953"
                              },
                              {
                                  name: "Mom", dob: "5/4/1956"
                              },
                              {
                                  name: "James & Jac", children:
                                      [
                                            {
                                                name: "James", dob: "2/19/1984"
                                            },
                                            {
                                                name: "Jaqueline", dob: "11/25/1986"
                                            }
                                      ]

                              },
                              {
                                  name: "Matthew", dob: "10/7/1988"
                              },
                              {
                                  name: "Andrew & Ashley", children:
                                        [
                                            {
                                                name: "Andrew", dob: "7/20/1986"
                                            },
                                            {
                                                name: "Ashley", dob: "3/21/1987"
                                            },
                                            {
                                                name: "Laura", dob: "4/11/2007"
                                            },
                                            {
                                                name: "Hannah", dob: "8/26/2010"
                                            }
                                        ]
                              }
                          ]
                    },
                    {
                        name: "Kathy & Jerry", children:
                            [
                                 {
                                     name: "Kathy", dob: "4/3/1966"
                                 },
                                 {
                                     name: "Jerry", dob: "4/29/1960"
                                 },
                                 {
                                     name: "Arielle", dob: "12/1/1998"
                                 },
                                 {
                                     name: "Tevis", dob: "12/1/1998"
                                 }
                            ]
                    },
                    {
                        name: "Eric & Theresa", children:
                            [
                                 {
                                     name: "Eric", dob: "5/6/1960"
                                 },
                                 {
                                     name: "Theresa ", dob: "7/19/1966"
                                 }
                            ]
                    },
                    { name: "Gayle", dob: "11/9/1956" },
                    { name: "Rangnar", dob: "10/3/1927" },
                    { name: "Grand Betty", dob : "6/21/1930"},
                    {
                        name: "Glenn & Debbie", children:
                            [
                                 {
                                     name: "Glenn", dob: "7/24/1951"
                                 },
                                 {
                                     name: "Debbie", dob: "1/12/1953"
                                 },
                                {
                                    name: "David", dob: "6/7/1981"
                                },
                                {
                                    name: "Sean & Jennifer", children:
                                        [
                                            {
                                                name: "Sean", dob: "5/13/1979"
                                            },
                                            {
                                                name: "Jennifer", dob: "8/17/1979"
                                            },
                                            {
                                                name: "Parker", dob: "1/6/2010"
                                            },
                                            {
                                                name: "Sybil", dob: "1/10/2013"
                                            }
                                        ]
                                }
                            ]
                    }


            ]
    };
    drawTreeMap(treeData);
});



function position() {
    this.style("left", function (d) { return d.x + "px"; })
        .style("top", function (d) { return d.y + "px"; })
        .style("width", function (d) { return Math.max(0, d.dx - 1) + "px"; })
        .style("height", function (d) { return Math.max(0, d.dy - 1) + "px"; });
}

