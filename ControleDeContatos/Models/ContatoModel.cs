using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        //Na Model é feita a validação e coleta de dados para ser encaminhado ao banco, são definidas as propriedades com o intuito de coletar e anexar os dados inseridos,
        //sendo coletados em uma classe que irá herdar essas propriedades 
        public int Id { get; set; }
       
        //requisição de invalidar a criação de um contato com o campo de nome vazio
        [Required(ErrorMessage = "Digite o nome do contato")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Digite o e-mail do contato")]
        //error para e-mails invalidos
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Digite o celular do contato")]
        [Phone(ErrorMessage = "O celular informado não é valido")]
        public string Celular { get; set; }
    }
}
