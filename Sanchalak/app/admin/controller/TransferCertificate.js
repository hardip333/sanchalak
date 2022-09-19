app.controller('TransferCertiController', function ($scope, $http, $rootScope, $window, $localStorage, $state, $cookies, $mdDialog, NgTableParams) {

    
    /*var qr;*/
    // $rootScope.pageTitle = "Manage Faculty";
    debugger;
    $scope.disable = false;
    $scope.qrcodeToImg = function () {
        // //debugger;
        canvas = document.getElementById("qrcode")
        /*dataurl = canvas.toDataURL();*/
        /*$scope.imgqrcode = document.getElementById("imgqrcode");*/
        document.getElementById('imgqrcode').src = canvas.toDataURL();
        //return dataurl;
    };


    $scope.printDiv = function (areaID, isConfirmTransfer) {
        //debugger;
        if (isConfirmTransfer == true) {
            //debugger;
            //var isConfirmTransfer = document.getElementById(ConfirmTransfer).value;
            var contents = document.getElementById(areaID).innerHTML;
            var othercontents = '<html>' + '<head><style type="text/css" media="print"> @page {size: A4 Portriat; margin: 2%;}  </style >' +
                '<link href="/../../bower_components/bootstrap/css/bootstrap.min.css" media="print" rel="stylesheet" type="text/css"></head>' + '<body style="border: 2px solid black">' + contents +
                '<script type = "text/javascript" > function printPage() { window.focus(); window.print(); return; }</script> ' +
                '</body></html>'
            var frame1 = document.createElement('iframe');
            frame1.name = "frame1";
            frame1.style.position = "absolute";
            frame1.style.top = "-1000000px";
            document.body.appendChild(frame1);

            frame1.contentWindow.contents = othercontents;
            frame1.src = 'javascript:window["contents"]';
            frame1.focus();
            setTimeout(function () {
                frame1.contentWindow.printPage();
            }, 200);
        }
        else {
            //debugger;
            alert('Please check the details and confirm the transfer of the student first.');
        }
    };
    //let printContents, popupWin;

    //popupWin = window.open('', '', 'top=0,left=0,height=100%,width=auto');
    //popupWin.document.open();
    //popupWin.document.write(`
    //  <html>
    //    <head>
    //    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
    //    </head>
    //    <style>

    //    </style>
    //<body onload="window.print();window.close()">${printContents}</body>
    //  </html>`
    //);
    //popupWin.document.close();


    //=================================


    //var printContents = document.getElementById(areaID).innerHTML;
    //   var popupWin = window.open('', '_self', 'width=300,height=300');
    //    popupWin.document.open();
    //    popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="style.css" /></head><body onload="window.print();window.close()">' + printContents + '</body></html>');
    //    popupWin.document.close();
    //}


    //try {
    //    //debugger;
    //    var ifrmPrint = document.createElement('iframe');
    //    var oIframe = document.getElementById(ifrmPrint);
    //    var oContent = document.getElementById(areaID).innerHTML;
    //    var oDoc = (oIframe.contentWindow || oIframe.contentDocument);
    //    if (oDoc.document) oDoc = oDoc.document;
    //    oDoc.write('<head><title>title</title>');
    //    oDoc.write('</head><body onload="this.focus(); this.print();">');
    //    oDoc.write(oContent + '</body>');
    //    oDoc.close();
    //} catch (e) {
    //    self.print();
    //}

    //seperator
    //setTimeout(function () {
    //    /*window.print()*/
    //    /*//debugger;*/
    //   var printContent = document.getElementById(areaID);
    //   /*var WinPrint = window.open('', '', 'width=900,height=650');*/
    //   // WinPrint.document.write(printContent.innerHTML);
    //   // //debugger;
    //   //WinPrint.document.close();

    //        WinPrint.focus();
    //        WinPrint.print(printContent.innerHTML);
    //        WinPrint.close();

    //}, 2000);

    //setTimeout(function () {
    //    visible();
    //}, 5000);


    //$scope.print23 = function () {
    //    //debugger;
    //    /*console.log("abc");*/
    //    /*alert('in');*/
    //    var divContents = document.getElementById("disp").innerHTML;

    //    var a = window.open('', 'Print ID-CARD');
    //    a.document.open();
    //    a.document.write('<html>');
    //    a.document.write('<body onload="window.print()">');

    //    a.document.write(divContents);
    //    a.document.write('</body></html>');
    //    a.document.close();
    //    //    // a.print();
    //}


    //$scope.printPageArea = function (areaID) {
    //    //debugger;
    //    var printContent = document.getElementById(areaID);
    //    var WinPrint = window.open('', '', 'width=900,height=650');
    //    WinPrint.document.write(printContent.innerHTML);
    //    WinPrint.document.close();
    //    WinPrint.focus();
    //    WinPrint.print();
    //    WinPrint.close();
    //}
    //=================================================================================================

   

    $scope.getProgrammeList = function () {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/TransferCerti/getProgrammeList',
            //data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                $scope.ProgrammeList = response.obj;
                //$localStorage.CertiListDisplay = $scope.CertiList;

            })
            .error(function (res) {
                //debugger;
                alert(res);
            });
    };

    $scope.getBranchList = function (ProgId) {
        //debugger;
        $http({
            method: 'POST',
            url: 'api/TransferCerti/getBranchList?ProgId=' + ProgId,
            //data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                $scope.BranchList = response.obj;
                //$localStorage.CertiListDisplay = $scope.CertiList;

            })
            .error(function (res) {
                //debugger;
                alert(res);
            });
    };

    $scope.enterDetails = function (PRN, ProgId, SpecId) {
        //debugger;
        $localStorage.ProgrammeId = ProgId;
        $localStorage.StudentData = {};
        $localStorage.StudentData.PRN = PRN;
        $localStorage.StudentData.ProgId = ProgId;
        $localStorage.StudentData.SpecId = SpecId; 
        $http({
            method: 'POST',
            url: 'api/TransferCerti/enterDetails?PRN=' + PRN + '&ProgId=' + ProgId,
            //data: $scope.ProgInst,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                alert('DATA STORED SUCCESSFULLY');
                $state.go('TransferCertificate');
                //$localStorage.CertiListDisplay = $scope.CertiList;

            })
            .error(function (res) {
                //debugger;
                alert(res);
            });
    };

    $scope.GenerateTransferCertificate = function () {
        //debugger;
        //let param = [];
        //param = getProgId();
        //$scope.ProgId = $localStorage.dashboardProgId;
        $localStorage.ProgrammeId = $localStorage.StudentData.ProgId;
        $http({
            method: 'POST',
            url: 'api/TransferCerti/GenerateTransferCertificate',
            data: $localStorage.StudentData,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                //debugger;
                $rootScope.showLoading = false;

                if (response.response_code == 601) {
                    alert('Transfer Certificate for this student is already generated.');
                    $state.go('GetStudentTransferDetails');
                }

                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                }
                else {
                    debugger;
                    $scope.ListOfRecords = response.obj;
                    $scope.len = response.obj.length;
                    $scope.firstRecord = $scope.ListOfRecords[0];
                    if ($scope.firstRecord.IsTransfered == true) {
                        $scope.disable = true;
                    }
                    $scope.lastRecord = $scope.ListOfRecords[$scope.len - 1];
                    //$scope.LastRecord = $scope.ListOfRecords[len - 1];

                    for (var i = 0; i < $scope.len; i++) {
                        if ($scope.ListOfRecords[i].PartStatus == "Complete" || $scope.ListOfRecords[i].PartStatus == "Pass") {
                            debugger;
                            $scope.ListOfRecords[i].Status = "Passed";
                            $scope.ListOfRecords[i].PartStatus = "Passing";
                        }

                        else if ($scope.ListOfRecords[i].PartStatus == "InComplete" || $scope.ListOfRecords[i].PartStatus == "Fail" || $scope.ListOfRecords[i].PartStatus == "Incomplete") {
                            debugger;
                            $scope.ListOfRecords[i].Status = "Failed";
                            $scope.ListOfRecords[i].PartStatus == "Failing";
                        }

                        else if ($scope.ListOfRecords[i].PartStatus == "" || $scope.ListOfRecords[i].PartStatus == undefined || $scope.ListOfRecords[i].PartStatus == null) {
                            debugger;
                            $scope.ListOfRecords[i].Status = "Appeared";
                            $scope.ListOfRecords[i].PartStatus == "Appearing";
                        }

                        else {
                            debugger;
                            $scope.ListOfRecords[i].PartStatus == "--";
                        }
                    }


                    //if ($scope.firstRecord.PartStatus == "Complete" || $scope.firstRecord.PartStatus == "Pass") {
                    //    //debugger;
                    //    $scope.Status = "Passed";
                    //    $scope.firstRecord.PartStatus = "Passing";
                    //}

                    //else if ($scope.firstRecord.PartStatus == "InComplete" || $scope.firstRecord.PartStatus == "Fail" || $scope.firstRecord.PartStatus == "Incomplete") {
                    //    //debugger;
                    //    $scope.Status = "Failed";
                    //    $scope.firstRecord.PartStatus == "Failing";
                    //}

                    //else if ($scope.firstRecord.PartStatus == "Appeared") {
                    //    $scope.Status = "Appeared";
                    //    $scope.firstRecord.PartStatus == "Appearing";
                    //}

                    //else {
                    //    //debugger;
                    //    $scope.firstRecord.PartStatus == "--";
                    //}

                    if ($scope.firstRecord.Gender == "Male") {
                        $scope.firstRecord.Gender = "He";
                        $scope.gpronoun = "his";
                    }
                    else if ($scope.firstRecord.Gender == "Female") {
                        $scope.firstRecord.Gender = "She";
                        $scope.gpronoun = "her";
                    }
                    else {
                        $scope.firstRecord.Gender = "He";
                        $scope.gpronoun = "his";
                    }

                    //for (let i = 0; i < $scope.len; i++) {
                    //    if ($scope.ListOfRecords[i].PartStatus == "Pass") {
                    //        $scope.ListOfRecords[i].PartStatus == "Passed";
                    //    }
                    //    else if ($scope.ListOfRecords[i].PartStatus == "Fail") {
                    //        $scope.ListOfRecords[i].PartStatus == "Failed";
                    //    }
                    //    else {
                    //        $scope.ListOfRecords[i].PartStatus == "--";
                    //    }
                    //}

                    //var today = new Date();
                    //$scope.date = today.getDate() + '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
                    //if ($scope.Student.IssueDate) {
                    //    let str1 = $scope.Student.IssueDate;
                    //    $scope.myArr1 = str1.split(" ");
                    //}
                    generateQRCode($scope.firstRecord.PRN, $localStorage.ProgrammeId);
                }
            })

            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            })
    };

    $scope.ConfirmTransfer = function (PRN, isConfirmTransfer) {
        //debugger;
        $scope.ProgrammeId = $localStorage.ProgrammeId;
        if (isConfirmTransfer == false) {
            alert('Please check the details and confirm the transfer of the student first.');
        }
        else {
            $http({
                method: 'POST',
                url: 'api/TransferCerti/ConfirmTransfer?PRN=' + PRN + '&isConfirmTransfer=' + isConfirmTransfer + '&ProgId=' + $scope.ProgrammeId,
                //data: $scope.ProgInst,
                headers: { "Content-Type": 'application/json' }
            })
                .success(function (response) {
                    //debugger;
                    alert('Transfer for the student Confirmed.Please print the Transfer Certificate');
                    //$localStorage.CertiListDisplay = $scope.CertiList;

                })
                .error(function (res) {
                    //debugger;
                    alert(res);
                });
        }
    };


    $scope.VerifyTransferCertificate = function () {
        //debugger;
        $scope.CertificateIssue2 = {};
        let param = getProgId();
        $scope.CertificateIssue2.PRN = param[0][0];
        $scope.CertificateIssue2.ProgrammeId = param[0][1];
        $scope.CertificateIssue2.CertiId = 3;
        $http({
            method: 'POST',
            url: 'api/TransferCerti/VerifyTransferCertificate',
            data: $scope.CertificateIssue2,
            headers: { "Content-Type": 'application/json' }
        })
            .success(function (response) {
                $rootScope.showLoading = false;
                if (response.response_code != "200") {
                    $rootScope.$broadcast('dialog', "Error", "alert", response.obj);
                    //changevisible1();
                    alert('DATA NOT FOUND!!');
                }
                else {
                    //debugger;
                    $scope.Student = response.obj;
                    if ($scope.Student.IssueDate) {
                        let str1 = $scope.Student.IssueDate;
                        $scope.myArr1 = str1.split(" ");
                    }
                }
            })

            .error(function (res) {
                $rootScope.$broadcast('dialog', "Error", "alert", res.obj);
            })
    };
});

