app.controller('ReconciliationCtrl', function ($scope, $http, $rootScope, $filter, $localStorage, $state, $cookies, Upload, $mdDialog, NgTableParams) {

    /*Declaration*/
    $scope.Reconciliation = {};
    $scope.Reconciliation.AppFee = {};
    $scope.Reconciliation.ExamFee = {};
    $scope.Reconciliation.AdmFee = {};
    
    /*Show-Hide Flags*/
    $scope.AppFees = false;
    $scope.AdmFees = false;
    $scope.ExamFees = false;
    $scope.AppReconcile = false;
    $scope.ExamReconcile = false;
    $scope.AdmReconcile = false;

    /*Get Data On The Basis Of PRN/UserName*/
    $scope.submit = function () {
        if ($scope.Reconciliation.PRN == null || $scope.Reconciliation.PRN == undefined || $scope.Reconciliation.PRN == "") {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Kindly Enter Applicant UserName / PRN")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
            $scope.IsTabVisible = false;
            $scope.AppFees = false;
            $scope.AdmFees = false;
            $scope.ExamFees = false;            
        }
        else {
            alert("Please proceed further for reconciliation...Thank You.");
            $scope.IsTabVisible = true;
            $scope.AppFees = false;
            $scope.AdmFees = false;
            $scope.ExamFees = false;
        }
    };

    /*Get Data of Application Fees*/
    $scope.ApplicationFees = function () {
        $scope.ExamFees = false;
        $scope.AdmFees = false;
        $scope.AdmReconcile = false;
        $scope.ExamReconcile = false;
        var data = { PRN: $scope.Reconciliation.PRN }
        $http({
            method: 'POST',
            url: 'api/Reconciliation/GetReconciliationDataForAppFees',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        $scope.AppFees = true;
                        $scope.ExamFees = false;
                        $scope.AdmFees = false;
                        $scope.ApplicationFeesTableParams = new NgTableParams({},
                            {
                                dataset: response.obj
                            });
                        $scope.ApplicationFeesDetails = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*For Expanding More Data With + Button - Application Fees*/
    $scope.expand_row = function (id) {
        let element = document.getElementById('expand' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col" + id).innerHTML = "+"
            element.add("collapse")
        }
    };

    /*Reconcile Button For Application Fees*/
    $scope.ShowApplication = function (data) {

        $scope.AppReconcile = true;
        $scope.AppOrderId = data;
        $(document).scrollTop($(document).height());
    };

    /*Application Fees Reconciliation*/
    $scope.AppFeeReconcile = function (data1, data2,data3) {

        data = {
            TransactionId: data1,
            BankReferanceNumber: data2,
            TransactionDate:data3,
            OrderId: $scope.AppOrderId
        };

        $http({
            method: 'POST',
            url: 'api/Reconciliation/ReconciliationOfApplicationFees',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    alert(response.obj);
                }
                else {

                    alert(response.obj);
                    $scope.Reconciliation.AppFee = {};
                    $scope.AppReconcile = false;
                    $scope.submit();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Application Fees Auto-Reconciliation*/
    $scope.AppAuto = function (OrderId) {
        
        var data = { order_id: OrderId }
        $http({
            method: 'POST',
            url: 'api/Reconciliation/AutoReconciliationApplicationFees',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.submit();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Get Data of Exam Fees*/
    $scope.ExaminationFees = function () {
        $scope.AppFees = false;
        $scope.AdmFees = false;
        $scope.AdmReconcile = false;
        $scope.AppReconcile = false;
        var data = { PRN: $scope.Reconciliation.PRN }
        $http({
            method: 'POST',
            url: 'api/Reconciliation/GetReconciliationDataForExamFees',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        $scope.ExamFees = true;
                        $scope.AppFees = false;
                        $scope.AdmFees = false;
                        $scope.ExamFeesTableParams = new NgTableParams({},
                            {
                                dataset: response.obj
                            });
                        $scope.ExamFeesDetails = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*For Expanding More Data With + Button - Exam Fees*/
    $scope.expand_row_exam = function (id) {
        let element = document.getElementById('expand_exam' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col_exam" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col_exam" + id).innerHTML = "+"
            element.add("collapse")
        }
    };

    /*Reconcile Button For Exam Fees*/
    $scope.ShowExam = function (data, PRN) {

        $scope.ExamReconcile = true;
        $scope.ExamOrderId = data.OrderId;
        $scope.ProgInstancePartTermId = data.ProgInstancePartTermId;
        $scope.ExamMasterId = data.ExamMasterId;
        $scope.PRN = PRN;
        $(document).scrollTop($(document).height());
    };

    /*Exam Fees Reconciliation*/
    $scope.ExamFeeReconcile = function (data1, data2,data3) {

        data = {
            TransactionId: data1,
            BankReferanceNumber: data2,
            TransactionDate: data3,
            OrderId: $scope.ExamOrderId,
            ProgInstancePartTermId: $scope.ProgInstancePartTermId,
            ExamMasterId: $scope.ExamMasterId,
            PRN: $scope.PRN
        };

        $http({
            method: 'POST',
            url: 'api/Reconciliation/ReconciliationOfExamFees',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    alert(response.obj);
                }
                else {

                    alert(response.obj);
                    $scope.Reconciliation.ExamFee = {};
                    $scope.ExamReconcile = false;
                    $scope.submit();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Exam Fees Auto-Reconciliation*/
    $scope.ExamAuto = function (OrderId) {

        var data = { order_id: OrderId }
        $http({
            method: 'POST',
            url: 'api/Reconciliation/AutoReconciliationExamFees',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.submit();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Get Data of Admission Fees*/
    $scope.AdmissionFees = function () {
        $scope.ExamFees = false;
        $scope.AppFees = false;
        $scope.ExamReconcile = false;
        $scope.AppReconcile = false;
        var data = { PRN: $scope.Reconciliation.PRN }
        $http({
            method: 'POST',
            url: 'api/Reconciliation/GetReconciliationDataForAdmFees',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        
                        $scope.AdmFees = true;
                        $scope.ExamFees = false;
                        $scope.AppFees = false;
                       
                        $scope.AdmFeesTableParams = new NgTableParams({},
                            {
                                dataset: response.obj
                            });
                        $scope.AdmFeesDetails1 = response.obj.Item1;
                        $scope.AdmFeesDetails2 = response.obj.Item2;
                        $scope.AdmFeesDetails3 = response.obj.Item3;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Get Data of Admission Fees - New*/
    $scope.AdmissionFees = function () {
        $scope.ExamFees = false;
        $scope.AppFees = false;
        var data = { PRN: $scope.Reconciliation.PRN }
        $http({
            method: 'POST',
            url: 'api/Reconciliation/GetReconciliationDataForAdmissionFees',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {

                        $scope.AdmFees = true;
                        $scope.ExamFees = false;
                        $scope.AppFees = false;

                        $scope.AdmissionFeesTableParams = new NgTableParams({},
                            {
                                dataset: response.obj
                            });
                        $scope.AdmissionFeesDetails = response.obj;
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*For Expanding More Data With + Button - Admission Fees - Payment Transaction*/
    $scope.expand_row_adm2 = function (id) {
        let element = document.getElementById('expand_adm2' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col_adm2" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col_adm2" + id).innerHTML = "+"
            element.add("collapse")
        }
    };

    /*For Expanding More Data With + Button - Admission Fees - Student Admission Fees*/
    $scope.expand_row_adm3 = function (id) {
        let element = document.getElementById('expand_adm3' + id).classList
        if (element.contains("collapse")) {
            document.getElementById("first_col_adm3" + id).innerHTML = "-"
            element.remove("collapse")
        } else {
            document.getElementById("first_col_adm3" + id).innerHTML = "+"
            element.add("collapse")
        }
    };

    /*Reconcile Button For Admission Fees*/
    $scope.ShowAdmission = function (data) {

        $scope.AdmReconcile = true;
        $scope.AdmOrderId = data;
       
        $(document).scrollTop($(document).height());
        
    };

    /*Admission Fees Reconciliation*/
    $scope.AdmFeeReconcile = function (data1, data2, data3) {
        debugger
        data = {
            TransactionId: data1,
            BankReferanceNumber: data2,
            TransactionDate: data3,
            OrderId: $scope.AdmOrderId
        };

        $http({
            method: 'POST',
            url: 'api/Reconciliation/ReconciliationOfAdmissionFees',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {

                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $state.go('login');

                } else if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    alert(response.obj);
                }
                else {

                    alert(response.obj);
                    $scope.Reconciliation.AdmFee = {};
                    $scope.AdmReconcile = false;
                    $scope.submit();
                }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

    /*Admission Fees Auto-Reconciliation*/
    $scope.AdmAuto = function (OrderId) {

        var data = { order_id: OrderId }
        $http({
            method: 'POST',
            url: 'api/Reconciliation/AutoReconciliationAdmissionFees',
            data: data,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code == "0") {
                    $go.state('login');
                }
                else
                    if (response.response_code != "200") {
                        $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        alert(response.obj);
                    }
                    else {
                        alert(response.obj);
                        $scope.submit();
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    };

});

app.directive('allowPattern', [allowPatternDirective]);

function allowPatternDirective() {
    return {
        restrict: "A",
        compile: function (tElement, tAttrs) {
            return function (scope, element, attrs) {

                element.bind("keypress", function (event) {
                    var keyCode = event.which || event.keyCode;
                    var keyCodeChar = String.fromCharCode(keyCode);

                    if (!keyCodeChar.match(new RegExp(attrs.allowPattern, "i"))) {
                        event.preventDefault();
                        return false;
                    }

                });
            };
        }
    };
}