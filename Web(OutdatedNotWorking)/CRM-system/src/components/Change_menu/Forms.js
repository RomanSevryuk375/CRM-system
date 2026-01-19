export const getInitialFormState = (formType) => {
  switch (formType) {
    case "car":
      return {
        brand: null,
        model: null,
        yearOfManufacture: null,
        vinNumber: null,
        stateNumber: null,
        mileage: null,
      };
    case "carWorker":
      return {
        mileage: null,
      };
    case "client":
      return {
        name: null,
        surname: null,
        email: null,
        phoneNumber: null,
      };
    case "expense":
      return {
        date: null,
        category: null,
        taxId: null,
        usedPartId: null,
        expenseType: null,
        sum: null,
      };
    case "order":
      return {
        statusId: null,
        date: null,
        priority: null,
      };
    case "paymentNote": 
      return {
        date: null,
        amount: null,
        method: null,
      };
    case "specialization":
      return {
        name: null,
      };
    case "supplier":
      return {
        name: null,
        contacts: null,
      };
    case "tax":
      return {
        name: null,
        rate: null,
        type: null,
      };
    case "usedPart":
      return {
        supplierId: null,
        name: null,
        article: null,
        quantity: null,
        unitPrice: null,
      };
    case "work":
      return {
        workerId: null,
        timeSpent: null,
        statusId: null,
      };
    case "workWorker":
      return {
        timeSpent: null,
        statusId: null,
      };
    case "worker":
      return {
        specializationId: null,
        name: null,
        surname: null,
        hourlyRate: null,
        email: null,
        phoneNumber: null,
      };
    case "workProposal":
      return {
        statusId: null,
        date: null,
      };
    case "workType":
      return {
        title: null,
        category: null,
        description: null,
        standardTime: null,
      };
    default:
      return {};
  }
};
