import { useEffect, useState } from 'react';
import { AgGridReact } from 'ag-grid-react';
import { ColDef } from 'ag-grid-community';
import { Switch } from "antd";
import { Product } from './model/product';
import { dateFilterParams } from './utilities/datefilter';
import ProductManagementAPI from './api/ProductManagementAPI';
import BarChart from './component/BarChart';
import './App.css';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';

function App() {

    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setIsLoading] = useState<boolean>(true);
    const [view, setView] = useState<string>("Products");

    const colDefs: ColDef<Product>[] = [
        { field: 'productName', headerName: 'Name', sortable: true, filter: true },
        { field: 'productCode', headerName: 'Product Code', sortable: true, filter: true },
        { field: 'categoryName', headerName: 'Category', sortable: true, filter: true },
        { field: 'stockQty', headerName: 'Stock Quantity', sortable: true, filter: true },
        {
            // format the price to include a £ symbol
            field: 'price', headerName: 'Price', sortable: true, filter: true,
            valueFormatter: ({ value }: { value: number }) => {
                return "\u00A3" + value.toFixed(2)
            }
        },
        {
            // format the date to user friendly version
            field: 'dateAdded', headerName: 'Date Added', sortable: true, filter: 'agDateColumnFilter',
            valueFormatter: ({ value }: { value: string }) => {
                const date = new Date(value);
                return date.toLocaleDateString('en-GB').split('T')[0];
            },
            filterParams: dateFilterParams
        }
    ];

    const productManagementAPI = new ProductManagementAPI();

    useEffect(() => {
        setIsLoading(true);
        productManagementAPI
            .getAllProducts()
            .then((res) => {
                setProducts(res);
            })
            .catch((error) => {
                console.error("Error retrieving products:", error);
            })
            .finally(() => {
                setIsLoading(false);
            });
    }, []);

    const toggleView = () => {
        view === "Products" ? setView("Stock") : setView("Products");
    };

    return (
        <div>
            <>
                {loading &&
                    <div>
                        Loading...
                    </div>
                }

                <Switch
                    defaultChecked
                    onChange={toggleView}
                    checkedChildren="Products"
                    unCheckedChildren="Stock"
                    style={{
                        width: "100px",
                        backgroundColor: view == "Products" ? 'green' : '#ffa500',
                        margin: "50px"
                    }}
                />
                <div className="mainContainer">
                    <>
                        {view == 'Products' &&
                            <div className="ag-theme-alpine ag-container">
                                <AgGridReact
                                    rowData={products}
                                    columnDefs={colDefs}
                                    pagination={true}
                                    paginationPageSize={10}
                                    paginationPageSizeSelector={[10, 20, 50, 100]}
                                />
                            </div>
                        }
                        {view == 'Stock' &&
                            <BarChart products={products}></BarChart>
                        }
                    </>
                </div>
            </>
        </div>
    );
}

export default App;