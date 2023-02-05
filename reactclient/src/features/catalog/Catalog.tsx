import { useState, useEffect } from "react";
import { Product } from "../../app/models/products";
import ProductList from "./ProductList";

function Catalog() {
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
        fetch('https://localhost:7158/api/product')
            .then(response => response.json())
            .then(data => setProducts(data))
    }, [])

    return (
        <>
            <ProductList products={products}/>
        </>
    )
}

export default Catalog;