import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

interface Departamento {
  id: number;
  nome: string;
  sigla: string;
  editing: boolean;
  confirmarExclusao: boolean;
}

@Component({
  selector: 'app-departamento',
  templateUrl: './departamento.component.html',
  styleUrls: ['./departamento.component.css']
})

export class DepartamentoComponent implements OnInit {

  departamentos: Departamento[] = [];

  novoDepartamento: Departamento = { id: 0, nome: '', sigla: '', editing: false, confirmarExclusao: false };

  headers = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.carregarDepartamentos();
  }

  carregarDepartamentos() {
    this.http.get<Departamento[]>('http://localhost:5027/api/Departamento', { headers: this.headers }).subscribe(
      data => {
        for (const depart of data) {
          depart.editing = false;
          depart.confirmarExclusao = false;
        }

        this.departamentos = data;
      },
      error => {
        alert('Erro ao carregar departamentos');
        console.error('Erro ao carregar departamentos:', error);
      }
    );
  }

  editarDepartamento(index: number) {
    this.departamentos[index].editing = true;
  }

  salvarDepartamento(index: number) {
    let departamento = this.departamentos[index];

    this.http.put<Departamento>("http://localhost:5027/api/Departamento/" + departamento.id, departamento, { headers: this.headers }).subscribe(
      response => {
        console.log('Requisição PUT bem-sucedida', response);
      }
    );
    this.departamentos[index].editing = false;
  }

  confirmarExclusao(index: number) {
    this.departamentos[index].confirmarExclusao = true;
  }

  excluirDepartamento(index: number) {
    let departamento = this.departamentos[index];

    this.http.delete<Departamento>("http://localhost:5027/api/Departamento/" + departamento.id, { headers: this.headers }).subscribe(
      response => {
        console.log('Requisição DELETE bem-sucedida', response);
      }
    );

    this.departamentos.splice(index, 1);
  }

  cadastrarNovoDepartamento() {
    if (this.novoDepartamento.nome && this.novoDepartamento.sigla) {
      this.http.post("http://localhost:5027/api/Departamento/", {
        nome: this.novoDepartamento.nome,
        sigla: this.novoDepartamento.sigla
      }, { headers: this.headers }).subscribe(
        response => {
          console.log('Requisição POST bem-sucedida', response);
        }
      );

      location.reload();
    }
  }
}
