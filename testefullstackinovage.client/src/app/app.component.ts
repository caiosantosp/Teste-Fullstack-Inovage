import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

interface BusinessPartners {
  cardName: string;
  city: string;
  country: string;
  cardCode: string;
  isEditing: boolean;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {
  public partners: BusinessPartners[] = [];
  public partner: BusinessPartners = {
    cardName: '',
    city: '',
    country: '',
    cardCode: '',
    isEditing: false
  };

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getPartners();
  }

  toggleEdit(partner: any) {
    if (partner.isEditing) {
      this.http.put(`${environment.apiUrl}/${partner.cardCode}`, partner).subscribe(
        () => {
          partner.isEditing = false;
          this.getPartners();
        },
        (error) => {
          console.error('Erro ao atualizar o parceiro', error);
        }
      );
    } else {
      partner.isEditing = true;
    }
  }

  createPartner(partner: any) {
    if (partner.cardName === '' || partner.city === '' || partner.country === '') {
      window.alert('Todos os campos são obrigatórios para adicionar um parceiro.');
      return;
    }

    this.http.post(`${environment.apiUrl}`, partner).subscribe(
      () => {
        window.alert(`Partner ${partner.cardName} adicionado.`);
        this.getPartners();
      },
      (error) => {
        console.error(error);
      }
    );
  };

  deletePartner(partner: any) {
    this.http.delete(`${environment.apiUrl}/${partner.cardCode}`).subscribe(
      () => {
        this.partners = this.partners.filter(p => p.cardCode !== partner.cardCode);
      },
      (error) => {
        console.error(error);
      }
    );
  };

  getPartners() {
    this.http.get<BusinessPartners[]>(`${environment.apiUrl}`).subscribe(
      (result) => {
        this.partners = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
