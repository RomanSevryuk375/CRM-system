export const getInitialFormState = (formType, clientId = null, workerId = null) => {
  switch (formType) {
    case "bill":
      return {
        orderId: "",
        statusId: "",
        date: "",
      };
    case "car":
      return {
        ownerId: clientId || "",
        brand: "",
        model: "",
        yearOfManufacture: "",
        vinNumber: "",
        stateNumber: "",
        mileage: 0,
      };
    case "client":
      return {
        roleId: "",
        name: "",
        surname: "",
        email: "",
        phoneNumber: "",
        login: "",
        password: "",
      };
    case "expense":
      return {
        date: "",
        category: "",
        taxId: "",
        usedPartId: "",
        expenseType: "",
        sum: 0,
      };
    case "order":
      return {
        statusId: "",
        carId: "",
        date: "",
        priority: "",
      };
    case "paymentNote": 
      return {
        billId: "",
        date: "",
        amount: 0,
        method: "",
      };
    case "specialization":
      return {
        name: "",
      };
    case "supplier":
      return {
        name: "",
        contacts: "",
      };
    case "tax":
      return {
        name: "",
        rate: 0,
        type: "",
      };
    case "usedPart":
      return {
        orderId: "",
        supplierId: "",
        name: "",
        article: "",
        quantity: 0,
        unitPrice: 0,
      };
    case "work":
      return {
        orderId: "",
        jobId: "",
        workerId: workerId || "",
        timeSpent: 0,
        statusId: "",
      };
    case "worker":
      return {
        roleId: "",
        specializationId: "",
        name: "",
        surname: "",
        hourlyRate: 0,
        email: "",
        phoneNumber: "",
        login: "",
        password: "",
      };
    case "workProposal":
      return {
        orderId: "",
        workId: "",
        byWorker: "",
        statusId: "",
        decisionStatusId: "",
        date: "",
      };
    case "workType":
      return {
        title: "",
        category: "",
        description: "",
        standardTime: 0,
      };
    default:
      return {};
  }
};
