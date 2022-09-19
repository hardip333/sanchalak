
app.controller('AdmRequiredDocumentsProgramCtrl', function ($scope, $http, $rootScope, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage Adm Required Documents Program";

    $scope.cardTitle = "Adm Required Documents Program Operation";
    $scope.rdocument = {};
    $scope.resetDocument = function () {
        $scope.rdocument = {};
    }

    $scope.getdocumentId = function () {
        $http({
            method: 'Post',
            url: 'api/MstDocument/MstDocumentGet',

            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.DocumentList = response.obj;

            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getprogramId = function () {
       
        $http({
            method: 'Post',
            url: 'api/MstProgramme/MstProgrammeListGetByIdPre',
            data: { ProgrammeInstancePartTermId: $localStorage.localObj.ProgrammeInstancePartTerm },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $scope.ProgrammeList = response.obj;
                for (let i = 0; i < $scope.ProgrammeList.length; i++) {
                   
                    $scope.rdocument.ProgrammeName = $scope.ProgrammeList[i].ProgrammeName;
                    $scope.rdocument.ProgramId = $scope.ProgrammeList[i].Id;
                   

                }
                            
            })
            .error(function (res) {
                alert(res);
            });
    };

    $scope.getPreDocumentListById = function () {
    
        $scope.ProgrammeInstanceName = $localStorage.localObj.ProgInstanceName;
        $scope.ProgrammeName = $localStorage.localObj.ProgrammeName;
        $scope.PartName = $localStorage.localObj.PartName;
        $scope.ProgrammePartTermName = $localStorage.localObj.ProgrammePartTermName;
        $http({
            method: 'Post',
            url: 'api/AdmRequiredDocumentsProgram/AdmRequiredDocumentsProgramGetByIdPre',
            data: { ProgrammeInstancePartTermId: $localStorage.localObj.ProgrammeInstancePartTerm },
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
               // $scope.DocumentListById = response.obj;
                $scope.rdocumentTableParams = new NgTableParams({
                }, {
                    dataset: response.obj
                });
                
            })
            .error(function (res) {
                alert(res);
            });
    };
    $scope.getPreDocumentListById();

    $scope.getDocumentList = function () {
     
        var data = new Object();
     

        $http({
            method: 'POST',
            url: 'api/AdmRequiredDocumentsProgram/AdmRequiredDocumentsProgramGet',
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
                    }
                    else {
                        $scope.AllDocumentList = response.obj;
                        //$scope.rdocumentTableParams = new NgTableParams({
                        //}, {
                        //    dataset: response.obj
                        //});
                    }
            })
            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            });
    }
    $scope.addDocument = function () {
        if ($scope.rdocument.ProgramId == null || $scope.rdocument.ProgramId === undefined ||
            $scope.rdocument.DocumentId == null || $scope.rdocument.DocumentId === undefined ||
            $scope.rdocument.DocumentType == null || $scope.rdocument.DocumentType === undefined ||
            $scope.rdocument.IsCompulsoryDocument == null || $scope.rdocument.IsCompulsoryDocument === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#AdmRequiredDocumentsProgramform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Add...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $scope.rdocument.ProgrammeInstancePartTermId = $localStorage.localObj.ProgrammeInstancePartTerm;
            $http({
                method: 'POST',
                url: 'api/AdmRequiredDocumentsProgram/AdmRequiredDocumentsProgramAdd',
                data: $scope.rdocument,
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
                        }
                        else {
                            alert(response.obj);
                            $scope.rdocument.DocumentId = {};
                            $scope.rdocument.DocumentType = {};
                            $scope.rdocument.IsCompulsoryDocument = {};
                            $scope.getPreDocumentListById();
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };
    $scope.editDocument = function () {

    
        if ($scope.rdocument.ProgramId == null || $scope.rdocument.ProgramId === undefined ||
            $scope.rdocument.DocumentId == null || $scope.rdocument.DocumentId === undefined ||
            $scope.rdocument.DocumentType == null || $scope.rdocument.DocumentType === undefined ||
            $scope.rdocument.IsCompulsoryDocument == null || $scope.rdocument.IsCompulsoryDocument === undefined
        ) {
            $scope.modifyUserFlag = true;
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#AdmRequiredDocumentsProgramform')))
                    .clickOutsideToClose(true)
                    .title("Error")
                    .textContent("Please Fill all Mandatory details before Edit...")
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Okay!')
            );
        }
        else {
            $scope.rdocument.ProgrammeInstancePartTermId = $localStorage.localObj.ProgrammeInstancePartTerm;
            $http({
                method: 'POST',
                url: 'api/AdmRequiredDocumentsProgram/AdmRequiredDocumentsProgramEdit',
                data: $scope.rdocument,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    //  $rootScope.showLoading = false;
                    if (response.response_code == "0") {
                        $go.state('login');
                    }
                    else
                        if (response.response_code != "200") {
                            $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                        }
                        else {
                            alert(response.obj);
                            $scope.rdocument.DocumentId = {};
                            $scope.rdocument.DocumentType = {};
                            $scope.rdocument.IsCompulsoryDocument = {};
                            $scope.getPreDocumentListById();
                            $scope.modifyUserFlag = false;
                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
        }
    };
    $scope.deleteDocument = function (ev, data) {
        var confirm = $mdDialog.confirm()
            .title('Would you like to delete?')
            .textContent('')
            .ariaLabel('Lucky day')
            .targetEvent(ev)
            .ok('Yes')
            .cancel('No');
        $mdDialog.show(confirm).then(function () {

            //$scope.newUser.createdById = $rootScope.id;
            $scope.rdocument = data;
            $http({
                method: 'POST',
                url: 'api/AdmRequiredDocumentsProgram/AdmRequiredDocumentsProgramDelete',
                data: $scope.rdocument,
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
                        }
                        else {
                            alert(response.obj);
                            $scope.rdocument = {};
                            $scope.getPreDocumentListById();

                        }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        },
            function () {
                $scope.status = 'You decided to keep your debt.';
            });
    };

    $scope.cancelDocument = function () {
        $scope.rdocument = {};
        $scope.modifyUserFlag = false;
    }

    $scope.modifyDocument = function (data) {
        $scope.rdocument = data;
        $scope.modifyUserFlag = true;
    }
    $scope.modifyMstDocumentData = function (data) {
        $scope.showFormFlag = true;
        $scope.rdocument = data;
        $scope.getPreDocumentListById();
    };

    $scope.displayDocument = function (data) {
        $scope.rdocument = data;
    };

    $scope.nextAdd = function () {
        $state.go('PreApplicationConfigurationEdit');
    };

    $scope.newDocumentAdd = function () {
        $state.go('AdmRequiredDocumentsProgramAdd');
    };

    $scope.backToList = function () {
        $state.go('AdmRequiredDocumentsProgramEdit');
    };

});