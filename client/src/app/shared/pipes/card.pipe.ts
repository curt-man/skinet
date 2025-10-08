import { Pipe, PipeTransform } from '@angular/core';
import { ConfirmationToken } from '@stripe/stripe-js';
import { PaymentSummary } from '../models/order';

@Pipe({
  name: 'card',
  standalone: true
})
export class CardPipe implements PipeTransform {

  transform(value?: ConfirmationToken['payment_method_preview'] | PaymentSummary, ...args: unknown[]): unknown {
    if(value && 'card' in value) {
      const {last4, brand, exp_year, exp_month} = (value as ConfirmationToken['payment_method_preview']).card!;
      return `${brand.toUpperCase()} **** **** **** ${last4}, Exp: ${exp_month}/${exp_year}`;
    } else if(value && 'last4' in value) {
      const {last4, brand, expYear, expMonth} = value as PaymentSummary;
      return `${brand.toUpperCase()} **** **** **** ${last4}, Exp: ${expMonth}/${expYear}`;
    } else {
      return 'Unknown payment method';
    }  }

}
