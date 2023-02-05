import { useEffect, useState } from "react";

function App() {

    const [products, setProducts] = useState([{name: "", price: 0.00}]);

    useEffect(() => {
        fetch('https://localhost:7158/api/product')
            .then(response => response.json())
            .then(data => setProducts(data))
    }, [])

  function addProduct() {
    setProducts(prevState => [...prevState, {name: 'product' + (prevState.length + 1), price: 300.50}])
  }

  return (
    <div>
      <h1>store</h1>
      <ul>
        {products.map((item, index) => (
          <li key={index}>{item.name} - {item.price}</li>
        ))}
      </ul>
      <button onClick={addProduct}>Add Product</button>
    </div>
  );
}

export default App;