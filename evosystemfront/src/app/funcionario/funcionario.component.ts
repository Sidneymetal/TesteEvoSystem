import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

interface Funcionario {
  id: number;
  nome: string;
  foto: string;
  rg: string;
  editing: boolean;
  confirmarExclusao: boolean;
}

interface DepartamentoResponse {
  listaFuncionarios: any[];
}

@Component({
  selector: 'app-funcionario',
  templateUrl: './funcionario.component.html',
  styleUrls: ['./funcionario.component.css']
})

export class FuncionarioComponent implements OnInit {

  funcionarios: Funcionario[] = [];

  novoFuncionario: Funcionario = { id: 0, nome: '', foto: '', rg: '', editing: false, confirmarExclusao: false };

  headers = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  departamentoId?: string;

  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router) {
    this.route.params.subscribe(params => this.departamentoId = params['departamentoId']);
  }

  ngOnInit() {
    this.carregarFuncionarios();
  }

  carregarFuncionarios() {
    this.http.get<DepartamentoResponse>('http://localhost:5027/api/Departamento/' + this.departamentoId, { headers: this.headers }).subscribe(
      data => {
        for (const func of data.listaFuncionarios) {
          func.editing = false;
          func.confirmarExclusao = false;
        }

        this.funcionarios = data.listaFuncionarios;
      },
      error => {
        alert('Erro ao carregar funcionarios');
        console.error('Erro ao carregar funcionarios:', error);
      }
    );
  }

  editarFuncionario(index: number) {
    this.funcionarios[index].editing = true;
  }

  salvarFuncionario(index: number) {
    let funcionario = this.funcionarios[index];

    this.http.put<Funcionario>("http://localhost:5027/api/Funcionario/" + funcionario.id, funcionario, { headers: this.headers }).subscribe(
      response => {
        console.log('Requisição PUT bem-sucedida', response);
      }
    );
    this.funcionarios[index].editing = false;
  }

  confirmarExclusao(index: number) {
    this.funcionarios[index].confirmarExclusao = true;
  }

  excluirFuncionario(index: number) {
    let funcionario = this.funcionarios[index];

    this.http.delete<Funcionario>("http://localhost:5027/api/Funcionario/" + funcionario.id, { headers: this.headers }).subscribe(
      response => {
        console.log('Requisição DELETE bem-sucedida', response);
      }
    );

    this.funcionarios.splice(index, 1);
  }

  cadastrarNovoFuncionario() {
    if (this.novoFuncionario.nome && this.novoFuncionario.foto && this.novoFuncionario.rg) {
      this.http.post("http://localhost:5027/api/Funcionario/", {
        nome: this.novoFuncionario.nome,
        foto: this.novoFuncionario.foto,
        rg: this.novoFuncionario.rg,
        departamentoId: this.departamentoId
      }, { headers: this.headers }).subscribe(
        response => {
          console.log('Requisição POST bem-sucedida', response);
        }
      );

      location.reload();
    }
  }

  voltarParaDepartamentos() {
    this.router.navigate(['/departamentos']);
  }
}
