import { useEffect, useState } from 'react';
import { Product } from '../model/product';
import ReactApexChart from "react-apexcharts";
import { ApexOptions } from "apexcharts";

interface BarChartProps {
    products: Product[]
}

function BarChart({ products }: BarChartProps) {
    const [categories, setCategories] = useState<string[]>([]);
    const [stockCounts, setStockCounts] = useState<number[]>([]);

    const options: ApexOptions = {
        chart: {
            type: 'bar',
            toolbar: {
                show: false,
                tools: {
                    download: false
                }
            },
            animations: {
                enabled: false
            },
        },
        grid: {
            show: false
        },
        colors: ['#ffa500'],
        tooltip: {
            enabled: false
        },
        states: {            
            hover: {
                filter: {
                    type: 'none'
                }
            },
            active: {
                allowMultipleDataPointsSelection: false,
                filter: {
                    type: 'none'
                }
            }
        },
        xaxis: {
            categories: categories
        }       
    };

    const series = [
        {
            name: 'Stock Count',
            data: stockCounts,
        },
    ];

    useEffect(() => {
        if (products.length < 1) {
            return
        }
        const groupedProducts = groupByCategoryName(products)
        const categories = Object.keys(groupedProducts);
        const stockCounts = categories.map((c => aggregateStockCount(groupedProducts[c])));
        setCategories(categories)
        setStockCounts(stockCounts)
    }, [products]);


    // Take collection of Products and return a collection of Category string keys
    // who's values are a collection of all Products that belong to that Category
    function groupByCategoryName(products: Product[]): { [key: string]: Product[] } {
        return products.reduce<{[key: string]: Product[]}>((acc, product) => {
            if (!acc[product.categoryName]) {
                acc[product.categoryName] = [];
            }
            acc[product.categoryName].push(product);
            return acc;
        }, {});
    }

    // Take an collection of Products and return sum of their collective Stock Quantity
    function aggregateStockCount(products: Product[]): number {
        const total = products.reduce((acc, product) => {
            return acc + product.stockQty;
        }, 0);
        return total
    }

    return (
        <>
            <div>Total Stock Per Category</div>
            <ReactApexChart
                options={options}
                series={series}
                type="bar"
                width={500}
                height={300}
            />
        </>
    );
}

export default BarChart;