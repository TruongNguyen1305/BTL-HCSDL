function Validator(formSelector){
    var _this=this;
    function getParent(element, selector){
        while(element.parentElement){
            if(element.parentElement.matches(selector)){
                return element.parentElement;
            }
            element=element.parentElement;
        }
    }

    var formRules={};
    var validatorRules={
        required: function(value){
            return value ? undefined : 'Vui lòng nhập trường này'
        },
        email: function(value){
            var regex=/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
            return regex.test(value) ? undefined : 'Trường này phải là email'
        },
        min: function(min){
            return function(value){
                return value.length >= min ? undefined : `Vui lòng nhập ít nhất ${min} kí tự`;
            }
        },
        max: function(max){
            return function(value){
                return value.length <= max ? undefined : `Vui lòng nhập nhiều nhất ${max} kí tự`;
            }
        },
        number: function (value) {
            var regex = /^(84|0[3|5|7|8|9])+([0-9]{8})\b$/;
            return regex.test(value) ? undefined : 'Trường này phải là SĐT';
        }
    };

    var ruleName='required';

    //Lay ra form element trong DOM
    var formElement=document.querySelector(formSelector);
    

    if(formElement){

        var inputs=formElement.querySelectorAll('[name][rules]');
        for(var input of inputs){

            var rules=input.getAttribute('rules').split('|');
            for(var rule of rules){
                var ruleInfo;
                var ruleHasValue=rule.includes(':');

                if(ruleHasValue){
                    ruleInfo=rule.split(':');
                    rule=ruleInfo[0];
                }

                var ruleFunc= validatorRules[rule];
                if(ruleHasValue){
                    ruleFunc=ruleFunc(ruleInfo[1]);
                }

                if(Array.isArray(formRules[input.name])){
                    formRules[input.name].push(ruleFunc);
                }
                else{
                    formRules[input.name]=[ruleFunc];
                }
            }
            //Lang nghe su kien de validate
            input.onblur=handleValidate;
            input.oninput=handleClearError;
        }
        function handleValidate(event){
            var rules = formRules[event.target.name];
            var errorMessage;

            for(var rule of rules){
                errorMessage = rule(event.target.value);
                if(errorMessage) break;
            }

            if(errorMessage){
                var formGroup= getParent(event.target,'.form-group');
                if(formGroup){
                    formGroup.classList.add('invalid');
                    var errorElement=formGroup.querySelector('.form-message')
                    if(errorElement){
                        errorElement.textContent=errorMessage;
                    }
                }
            }
            return !errorMessage;
        }
        function handleClearError(event){
            var formGroup= getParent(event.target,'.form-group');
            if(formGroup.classList.contains('invalid')){
                formGroup.classList.remove('invalid');
                var errorElement=formGroup.querySelector('.form-message')
                if(errorElement){
                    errorElement.textContent='';
                }
            }
        }
    }
    //Xu li hanh vi submit
    formElement.onsubmit=function(event){
        event.preventDefault();
        var inputs=formElement.querySelectorAll('[name][rules]');
        var isValid = true;
        for(var input of inputs){
            if(!handleValidate({ target: input})){
                isValid=false;
            }
        }

        if(isValid){
            if(typeof _this.onSubmit==='function'){
                var enableInputs = formElement.querySelectorAll('[name]:not([disabled])');
                var formValues=Array.from(enableInputs).reduce(function(value, input){
                    switch(input.type){
                        case 'radio':
                            if(input.matches(':checked')){
                                value[input.name]=input.value;
                            }
                            break;
                        case 'checkbox':
                            if(!input.matches(':checked')) {
                                return value;
                            }
                            if(!Array.isArray(value[input.name])){
                                value[input.name]=[input.value];
                            }
                            else{
                                value[input.name].push(input.value);
                            }
                            break;
                        case 'file':
                            value[input.name]=input.files;
                            break;
                        default:
                            value[input.name]=input.value;
                    }
                    return value;
                }, {});
            }

            formElement.submit();
        }
    }
}