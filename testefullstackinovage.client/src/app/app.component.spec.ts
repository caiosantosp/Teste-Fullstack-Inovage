import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [HttpClientTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('should retrieve business partners from the server', () => {
    const mockPartners = [
      { cardName: 'Teste 404', city: 'Barueri', country: 'São Paulo', cardCode: '31', isEditing: true },
      { cardName: 'Teste 500', city: 'Cajamar', country: 'São Paulo', cardCode: '43', isEditing: true }
    ];

    component.ngOnInit();

    const req = httpMock.expectOne('/businesspartners');
    expect(req.request.method).toEqual('GET');
    req.flush(mockPartners);

    expect(component.partners).toEqual(mockPartners);
  });
});
