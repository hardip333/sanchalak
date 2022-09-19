app.controller('HallticketInstructionConfigurationCtrl', function ($scope, Upload,$window, $http, $filter, $localStorage, $rootScope, $state, $cookies, $mdDialog, NgTableParams) {

    $rootScope.pageTitle = "Manage HallTicket Instruction Configuration";
    // Declaration Starts
    var obj;
    var today = new Date();
    var date = today.getDate();
    var month = today.getMonth() + 1;
    var year = today.getFullYear();   
    $scope.showImg = false;
    //Declaration Ends
    $scope.IsVisibleSubmitBtn = function () {
      
        $scope.IsSubmitBtnVisible = true;
        $scope.IsHallTicketVisible = false;
        
    }
    /*Reset HallticketInstConfig*/
    $scope.cancelHallticketInstConfig = function () {
        $scope.IsHallTicketVisible = false;
        $scope.IsSubmitBtnVisible = false;
        $scope.HallTicket = {};
        
    };

   

    /*Get ExamEvent Get List*/
    $scope.ExamEventGet = function () {

        $http({
            method: 'POST',
            url: 'api/HallticketInstructionConfiguration/ExamEventMasterListGet',
            //data: SubSpecialisation,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.FacList = {};
                }
                else {
                
                    $scope.ExamEventList = response.obj;                  
                   
                }
                

            })
            .error(function (res) {

            });
    };

    /*Get ExamEventGetById */
    $scope.ExamEventGetById = function () {

        $http({
            method: 'POST',
            url: 'api/HallticketInstructionConfiguration/ExamEventMasterListGetById',
            data: $scope.HallTicket,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                if (response.response_code == "201") {
                    $scope.FacList = {};
                }
                else {
                   
                    $scope.ExamEventListById = response.obj[0];
                    $scope.HallTicket.MandatoryInstructionForOnlineExam = $scope.ExamEventListById.MandatoryInstructionForOnlineExam;
                    $scope.HallTicket.OptionalInstructionForOnlineExam = $scope.ExamEventListById.OptionalInstructionForOnlineExam;
                    $scope.HallTicket.MandatoryInstructionForOfflineExam = $scope.ExamEventListById.MandatoryInstructionForOfflineExam;
                    $scope.HallTicket.OptionalInstructionForOfflineExam = $scope.ExamEventListById.OptionalInstructionForOfflineExam;
                    $scope.HallTicket.EditStatus = $scope.ExamEventListById.EditStatus;
                    $scope.HallTicket.IsClosed = $scope.ExamEventListById.IsClosed;
                    $scope.HallTicket.DyrExamSign = $scope.ExamEventListById.DyrExamSign;
                    $scope.showImg = false;
                    $scope.showImgGet = true;

                  


                }


            })
            .error(function (res) {

            });
    };

    $scope.GetHallTicketInstructionConfiguration = function () {    
        $scope.ExamEventGetById();
        $scope.IsHallTicketVisible = true;
        $scope.IsSubmitBtnVisible = false;

    }

    /*Add HallTicketInstructionConfiguration*/
    $scope.HallTicketInstructionConfigurationAdd = function () {
        var ManOnlineFlag = document.getElementById("MOn").value.length;
        var OptOnlineFlag = document.getElementById("OOn").value.length;
        var ManOfflineFlag = document.getElementById("MOff").value.length;
        var OptOfflineFlag = document.getElementById("OOff").value.length;
       
        if (ManOnlineFlag != 0 && OptOnlineFlag == 0)
        {
       
          $mdDialog.show(
                     $mdDialog.alert()
                         .parent(angular.element(document.querySelector('#popupContainer')))
                         .clickOutsideToClose(true)
                         .textContent('Please enter Optional Online Instructions ...')
                         .ok('Got it!')
               
         );

            
        }
        else if (ManOnlineFlag == 0 && OptOnlineFlag != 0) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .textContent('Please enter Mandatory Online Instructions ...')
                    .ok('Got it!')

            );
        }

        else if (ManOfflineFlag == 0 && OptOfflineFlag != 0) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .textContent('Please enter Mandatory Offline Instruction...')
                    .ok('Got it!')

            );
        }

        else if (OptOfflineFlag == 0 && ManOfflineFlag != 0) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .textContent('Please enter Optional Offline Instruction...')
                    .ok('Got it!')

            );
        }
        else if (ManOnlineFlag == 0 && OptOnlineFlag == 0 && OptOfflineFlag == 0 && ManOfflineFlag == 0) {
            $mdDialog.show(
                $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .textContent('Please Enter either Online or Offline Instruction...')
                    .ok('Got it!')

            );
        }
       
        else if ($scope.HallTicket.DyrExamSign == null || $scope.HallTicket.DyrExamSign == undefined || $scope.HallTicket.DyrExamSign == "") {
        
                 $mdDialog.show(
                     $mdDialog.alert()
                         .parent(angular.element(document.querySelector('#popupContainer')))
                         .clickOutsideToClose(true)
                         .textContent('You have not Uploaded Dyr / Exam / Academics Signature...')
                         .ok('Got it!')
               
         );

        }
        else {
            $http({
                method: 'POST',
                url: 'api/HallticketInstructionConfiguration/HallticketInstructionConfigurationUpdate',
                data: $scope.HallTicket,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    $rootScope.showLoading = false;

                    if (response.response_code == "0") {
                        $state.go('login');

                    } else if (response.response_code != "200") {
                       
                        $scope.IsHallTicketVisible = false;
                        $scope.IsSubmitBtnVisible = false;
                        alert(response.obj);
                        $scope.HallTicket = {};
                    }
                    else {
                        $scope.IsHallTicketVisible = false;
                        $scope.IsSubmitBtnVisible = false;
                        document.getElementById("SuccessMsgSignature").innerHTML = null;                     
                        $scope.HallTicket = {};
                        $("#HallTicket_Signature").val("");
                        alert(response.obj);
                        $window.onload();
                       


                    }
                })
                .error(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });

        }
        
           

       
    };


    $scope.byCode = function (data) {
       
        return $http({
            method: 'POST',
            url: 'api/HallticketInstructionConfiguration/GenericConfigurationGetByShortCode',
            data: { ShortCode: data },
            headers: { "Content-Type": 'application/json' }
        })
    };

    $scope.UploadHallTicketSignature = function ($files) {
      
        $scope.showImg = false;
        $scope.showImgGet = false;
       
        $scope.SelectedFiles = $files;
        var fileExtension = '.' + $scope.SelectedFiles[0].name.split('.').pop();
        $scope.DyrExamSign = "HallTicketInsSignature" + "_" + date + "_" + month + "_" + year + fileExtension;
        $cookies.put("HallTicketInsSignature", $scope.DyrExamSign);
       
        if ($scope.SelectedFiles && $scope.SelectedFiles.length) {

            var txtHT = document.getElementById("HallTicket_Signature").value;
            var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.bmp)$/i;
            const fi = document.getElementById("HallTicket_Signature");

            if (txtHT == "" || txtHT === undefined) {
                document.getElementById("SuccessMsgSignature").innerHTML = "";
                document.getElementById("ErrorMsgSignature").innerHTML = "Must upload the file";
                alert("Must upload the file");
                return false;
            }
            else if (!allowedExtensions.exec(txtHT)) {
                document.getElementById("ErrorMsgSignature").innerHTML = "It only accepts jpeg and png image";
                document.getElementById("SuccessMsgSignature").innerHTML = "";
                alert("It only accepts jpeg/jpg and png image");
                txtHT.value = '';
                $("#HallTicket_Signature").val("");
                $scope.showImg = false;
                $scope.showImgGet = false;
                return false;
            }
            else if (fi.files.length > 0) {
                var cnt = fi.files.length;
                $scope.byCode('HallTicketSignature').then(function (response) {
                    $scope.maxVal = response.data.obj[0].Value;
                    for (i = 0; i < cnt; i++) {
                        const fsize = fi.files.item(i).size;
                        const file = Math.round((fsize / 1024));
                      
                        if (file >= $scope.maxVal) {
                            document.getElementById("ErrorMsgSignature").innerHTML = "Please select a file below 50KB";
                            document.getElementById("SuccessMsgSignature").innerHTML = "";
                            alert("Please select a file below 50KB");
                            $("#HallTicket_Signature").val("");
                            $scope.showImg = false;
                            $scope.showImgGet = false;
                            return false;
                        }
                        else {

                            
                            Upload.upload({
                                url: 'api/HallticketInstructionConfiguration/UploadHallTicketSignature',
                                data: {
                                    files: $scope.SelectedFiles
                                }
                            }).then(function (response) {
                                if (response.data.response_code == "200") {
                                   
                                    //$scope.HallTicket.DyrExamSign = null;
                                    $scope.HallTicket.DyrExamSign = response.data.obj;
                                   
                                    $scope.showImg = true;
                                    $scope.showImgGet = false;
                                    document.getElementById("ErrorMsgSignature").innerHTML = "";
                                    document.getElementById("SuccessMsgSignature").innerHTML = "File Uploaded Successfully..!";
                                }
                                else if (response.data.response_code == "0") {

                                    alert(response.data.obj);
                                    $state.go('login');
                                }
                                else {

                                    $scope.DyrExamSign = undefined;
                                    $scope.HallTicket.DyrExamSign = undefined;
                                    $("#HallTicket_Signature").val("");
                                    alert("File Upload Failed!");
                                    return false;
                                }




                            }, function (response) {
                                if (response.status > 0) {
                                    var errorMsg = response.status + ': ' + response.data;

                                }
                            }, function (evt) {
                                var element = angular.element(document.querySelector('#dvProgress'));
                                $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                                element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                            });
                        }
                    }
                }).catch(function (res) {
                    $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
                });
            }

        }
    };

    
    
    

  

    

});

