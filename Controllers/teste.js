const produtos = [
  {
    nome: "notebook",
    preco: 2100
  },
  {
    nome: "smartphone",
    preco: 400
  }
]

const produtosEmDolar = produtos.map(p => p.preco * 4)
const total = produtos.reduce((a, b) => a + b.preco, 0)

console.log(produtos);
console.log(produtosEmDolar);
console.log(total);