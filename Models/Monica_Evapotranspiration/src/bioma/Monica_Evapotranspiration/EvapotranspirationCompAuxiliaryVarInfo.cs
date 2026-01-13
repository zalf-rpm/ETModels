
using System;
using System.Collections.Generic;
using CRA.ModelLayer.Core;
using System.Reflection;
using CRA.ModelLayer.ParametersManagement;   

namespace EvapotranspirationComp.DomainClass
                                {
                                    public class EvapotranspirationCompAuxiliaryVarInfo : IVarInfoClass
                                    {

                                        static EvapotranspirationCompAuxiliaryVarInfo()
                                        {
                                            EvapotranspirationCompAuxiliaryVarInfo.DescribeVariables();
                                        }

                                        public virtual string Description
                                        {
                                            get { return "EvapotranspirationCompAuxiliary Domain class of the component";}
                                        }

                                        public string URL
                                        {
                                            get { return "http://" ;}
                                        }

                                        public string DomainClassOfReference
                                        {
                                            get { return "EvapotranspirationCompAuxiliary";}
                                        }

                                        static void DescribeVariables()
                                        {
                                        }

                                    }
                                }