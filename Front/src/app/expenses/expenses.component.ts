import { Component } from '@angular/core';
import { NgbPopoverConfig } from '@ng-bootstrap/ng-bootstrap';
import { UtilitiesService } from '../services/utilities.service';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.scss']
})
export class ExpensesComponent {
  days = Array.from({length: 30}, (_, index) => index + 1);
  expensesForm: FormGroup;
  selectedCategoryDay?: any; //TO DO dodać typ
  selectedCategoryId: any;
  selectedSubcategoryId?: any;
  selectedDay: any;
  newExpenseAmount: any;
  newExpenseComment: any
  savingExpensesId?: string;

  constructor(private utilitiesService: UtilitiesService, private formBuilder: FormBuilder) {
    //safesub
    this.utilitiesService.getIsExpensesPopoverVisible().subscribe(isVisible => {
      if (!isVisible) {
        var popover = document.getElementById("custom-popover")!;
        popover.style.display = 'none';
      }
    });

    this.expensesForm = new FormGroup({
      amount: new FormControl(''),
      comment: new FormControl('')
    });
  }

  data = [
    {
      id: 'e3f859bc-8eda-4b9a-aa20-d454db7b7669',
      name: 'Dom',
      subcategories: [
        {
          id: '01074c9b-c510-433c-9b83-0cb423e7c5ee',
          name: 'Czynsz',
          categoryDays: [
            {
              id: '45fc7a02-4ddc-4522-b50f-1929e72b39d3',
              dayOfMonth: 2,
              amount: 25.34,
              expenses: [
                {
                  id: '79b5fee2-0f7f-4b6f-94f1-65278bb5d809',
                  amount: 10.0,
                  comment: 'Lody w Latto Gelatto'
                },
                {
                  id: 'c3fd0760-3dee-4f18-9fac-a8bf28462b21',
                  amount: 15.34,
                  comment: 'Zakupy'
                }
              ]
            },
            {
              id: '45fc7a02-4ddc-4522-b50f-1929e72b39d3',
              dayOfMonth: 5,
              amount: 25.34,
              expenses: [
                {
                  id: '79b5fee2-0f7f-4b6f-94f1-65278bb5d809',
                  amount: 10.0,
                  comment: 'Lody w Latto Gelatto'
                },
                {
                  id: 'c3fd0760-3dee-4f18-9fac-a8bf28462b21',
                  amount: 15.34,
                  comment: 'Zakupy'
                }
              ]
            },
            {
              id: '45fc7a02-4ddc-4522-b50f-1929e72b39d3',
              dayOfMonth: 15,
              amount: 25.34,
              expenses: [
                {
                  id: '79b5fee2-0f7f-4b6f-94f1-65278bb5d809',
                  amount: 10.0,
                  comment: 'Lody w Latto Gelatto'
                },
                {
                  id: 'c3fd0760-3dee-4f18-9fac-a8bf28462b21',
                  amount: 15.34,
                  comment: 'Zakupy'
                }
              ]
            },
            {
              id: '45fc7a02-4ddc-4522-b50f-1929e72b39d3',
              dayOfMonth: 22,
              amount: 25.34,
              expenses: [
                {
                  id: '79b5fee2-0f7f-4b6f-94f1-65278bb5d809',
                  amount: 10.0,
                  comment: 'Lody w Latto Gelatto'
                },
                {
                  id: 'c3fd0760-3dee-4f18-9fac-a8bf28462b21',
                  amount: 15.34,
                  comment: 'Zakupy'
                }
              ]
            }
          ]
        },
        {
          id: '171cbd93-ac6c-410f-a982-cb3936230b0f',
          name: 'Opłaty'
          
        },
        {
          id: 'f2e5e8d1-bf06-4751-b3c1-168e9af41e5d',
          name: 'Naprawy'
        },
        {
          id: '89460aa6-4838-472d-8108-2c888969f469',
          name: 'Inne'
        },
      ]
    },
    {
      id: 'e3f859bc-8eda-4b9a-aa20-d454db7b7669',
      name: 'Dom',
      subcategories: [
        {
          id: '01074c9b-c510-433c-9b83-0cb423e7c5ee',
          name: 'Czynsz',
          categoryDays: [
            {
              id: '45fc7a02-4ddc-4522-b50f-1929e72b39d3',
              dayOfMonth: 2,
              amount: 25.34,
              expenses: [
                {
                  id: '79b5fee2-0f7f-4b6f-94f1-65278bb5d809',
                  amount: 10.0,
                  comment: 'Lody w Latto Gelatto'
                },
                {
                  id: 'c3fd0760-3dee-4f18-9fac-a8bf28462b21',
                  amount: 15.34,
                  comment: 'Zakupy'
                }
              ]
            },
            {
              id: '45fc7a02-4ddc-4522-b50f-1929e72b39d3',
              dayOfMonth: 5,
              amount: 25.34,
              expenses: [
                {
                  id: '79b5fee2-0f7f-4b6f-94f1-65278bb5d809',
                  amount: 10.0,
                  comment: 'Lody w Latto Gelatto'
                },
                {
                  id: 'c3fd0760-3dee-4f18-9fac-a8bf28462b21',
                  amount: 15.34,
                  comment: 'Zakupy'
                }
              ]
            },
            {
              id: '45fc7a02-4ddc-4522-b50f-1929e72b39d3',
              dayOfMonth: 15,
              amount: 25.34,
              expenses: [
                {
                  id: '79b5fee2-0f7f-4b6f-94f1-65278bb5d809',
                  amount: 10.0,
                  comment: 'Lody w Latto Gelatto'
                },
                {
                  id: 'c3fd0760-3dee-4f18-9fac-a8bf28462b21',
                  amount: 15.34,
                  comment: 'Zakupy'
                }
              ]
            },
            {
              id: '45fc7a02-4ddc-4522-b50f-1929e72b39d3',
              dayOfMonth: 22,
              amount: 25.34,
              expenses: [
                {
                  id: '79b5fee2-0f7f-4b6f-94f1-65278bb5d809',
                  amount: 10.0,
                  comment: 'Lody w Latto Gelatto'
                },
                {
                  id: 'c3fd0760-3dee-4f18-9fac-a8bf28462b21',
                  amount: 15.34,
                  comment: 'Zakupy'
                }
              ]
            }
          ]
        },
        {
          id: '171cbd93-ac6c-410f-a982-cb3936230b0f',
          name: 'Opłaty'
          
        },
        {
          id: 'f2e5e8d1-bf06-4751-b3c1-168e9af41e5d',
          name: 'Naprawy'
        },
        {
          id: '89460aa6-4838-472d-8108-2c888969f469',
          name: 'Inne'
        },
      ]
    }
  ]

  getCategory(categoryId: string) {
    return this.data.find(x => x.id == categoryId);
  }

  getSubcategory(categoryId: string, subcategoryId: string) {
    return this.getCategory(categoryId)?.subcategories.find(x => x.id == subcategoryId);
  }

  getSubcategoryDay(categoryId: string, subcategoryId: string, day: number) {
    return this.getSubcategory(categoryId, subcategoryId)?.categoryDays?.find(x => x.dayOfMonth == day);
  }

  categories = [
    {
      name: 'Dom',
      isSubcategory: false,
      id: 'e3f859bc-8eda-4b9a-aa20-d454db7b7669'
    },
    {
      name: 'Czynsz',
      isSubcategory: true,
      id: '01074c9b-c510-433c-9b83-0cb423e7c5ee'
    },
    {
      name: 'Opłaty',
      isSubcategory: true,
      id: '171cbd93-ac6c-410f-a982-cb3936230b0f'
    },
    {
      name: 'Naprawy',
      isSubcategory: true,
      id: 'f2e5e8d1-bf06-4751-b3c1-168e9af41e5d'
    },
    {
      name: 'Inne',
      isSubcategory: true,
      id: '89460aa6-4838-472d-8108-2c888969f469'
    },
    {
      name: 'Transport',
      isSubcategory: false,
      id: '1ae66e32-083b-4e73-b2ea-f317d4d2ebe2'
    },
    {
      name: 'Paliwo',
      isSubcategory: true,
      id: 'b17314c5-2429-45eb-bd36-2169cd8ab9bf'
    },
    {
      name: 'Ubezpieczenie',
      isSubcategory: true,
      id: '50105665-cdf2-446d-94d4-5dbabdd10f00'
    },
    {
      name: 'Przegląd',
      isSubcategory: true,
      id: '76162146-bc7c-4b16-b34c-514fcdb35236'
    },
    {
      name: 'Bilety',
      isSubcategory: true,
      id: '3273f056-ed20-4b76-b1c4-3e3a3e37f36a'
    },
    {
      name: 'Inne',
      isSubcategory: true,
      id: '8eb969ad-65d4-4f2f-93e8-bb2487016523'
    },
    {
      name: 'Zdrowie',
      isSubcategory: false,
      id: 'a2eccc89-129d-411f-bc51-6966de309dbf'
    },
    {
      name: 'Badania',
      isSubcategory: true,
      id: '4e3842b8-3430-4e21-9ec4-2143d5c7317a'
    },
    {
      name: 'Odżywki',
      isSubcategory: true,
      id: 'cc36eecd-1a58-4a62-a396-5fb765541ed6'
    },
    {
      name: 'Zabiegi',
      isSubcategory: true,
      id: '298e021d-dccc-47e8-ac93-59dc7ee16992'
    },
    {
      name: 'Inne',
      isSubcategory: true,
      id: '5ffcfe66-e89a-4fb9-9ac0-5afa47470536'
    },
    {
      name: 'Rozwój',
      isSubcategory: false,
      id: 'db733ede-9193-4d7d-aabb-fc4a6c24adda'
    },
    {
      name: 'Książki',
      isSubcategory: true,
      id: 'edc32c9e-ac2f-4c60-b9ef-fb48678e95f8'
    },
    {
      name: 'Szkolenia',
      isSubcategory: true,
      id: '6227152c-7328-45a0-9b46-0c8cb6077145'
    },
    {
      name: 'Inne',
      isSubcategory: true,
      id: 'f2678906-b2fb-44f8-826e-ac4bf2a79a7b'
    },
  ]

  saveValue(event: any) {
    console.log(event.target.value);
  }

  showExpensesPopup(event: any, categoryId: string, subcategoryId: string, day: number) {
    this.newExpenseAmount = null;
    this.newExpenseComment = null;

    this.selectedCategoryId = categoryId;
    this.selectedSubcategoryId = subcategoryId;
    this.selectedDay = day;
    this.selectedCategoryDay = this.getSubcategoryDay(categoryId, subcategoryId, day);;
   
    var popover = document.getElementById("custom-popover")!;
    popover.style.top = event.clientY + 'px';
    popover.style.left = event.clientX + 'px';
    popover.style.display = 'block';
    event.target.classList.add("table-success");
  }

  addExpense(event: any) {
    let newExpense = {
      amount: event.target.value,
      id: '6a2eff3a-a550-4c98-bb99-fb3da3baa333',
      comment: ''
    }

    if (this.selectedCategoryDay != null) {
      this.selectedCategoryDay.expenses.push(newExpense);
      this.selectedCategoryDay.amount = +newExpense.amount + +this.selectedCategoryDay.amount;
    }
    else {
      //dodanie categoryDay do bazy;
      //podanie subcategoryId + informacji o wydatku

      const categoryDay = {
        id: '524cef47-12b5-4031-a5da-6f8762683492',
        dayOfMonth: this.selectedDay,
        amount: event.target.value,
        expenses: [
          {
            id: '82ed6115-ae01-4662-b5ee-e0be9d616bae',
            amount: event.target.value,
            comment: ''
          }
        ]
      }

      //Może lepiej przechowywać referencje do category i subcategory zmiast id
      const subcategory = this.getSubcategory(this.selectedCategoryId, this.selectedSubcategoryId); 
      subcategory!.categoryDays = [categoryDay];

      this.selectedCategoryDay = this.getSubcategoryDay(this.selectedCategoryId, this.selectedSubcategoryId, this.selectedDay);
    }
    
    this.newExpenseAmount = null;
  }

  updateExpense(event: any, expenseId: string) {
    const expense = this.selectedCategoryDay.expenses.find((x: any) => x.id == expenseId);
    
    this.selectedCategoryDay.amount -= expense.amount; 
    this.selectedCategoryDay.amount += +event.target.value;
    expense.amount = event.target.value;
  }

  updateComment(event: any, expenseId: string) {
    const expense = this.selectedCategoryDay.expenses.find((x: any) => x.id == expenseId);
    expense.comment = event.target.value;
  }
}
