﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.17929
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PacMan.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PacMan.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #######################
        ///#..........#..........#
        ///#.###.####.#.####.###.#
        ///#*###.####.#.####.###*#
        ///#.....................#
        ///#.###.#.#######.#.###.#
        ///#.....#....#....#.....#
        ///#####.#### # ####.#####
        ///#   #.#         #.#   #
        ///#####.# ###=### #.#####
        ///#    .  # === #  .    #
        ///#####.# ####### #.#####
        ///#   #.#    %    #.#   #
        ///#####.# ####### #.#####
        ///#..........#..........#
        ///#.###.####.#.####.###.#
        ///#*..#......\......#..*#
        ///###.#.#.#######.#.#.###
        ///#.....#....#....#.....#
        ///#.########.#.########.#
        ///#........... [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string level1 {
            get {
                return ResourceManager.GetString("level1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на ###################
        ///#......#= =#......#
        ///#.##...## ##...##.#
        ///#.#*.#%.....%#.*#.#
        ///#.##.#.#####.#.##.#
        ///#........\........#
        ///###################.
        /// </summary>
        internal static string level2 {
            get {
                return ResourceManager.GetString("level2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на #####################################
        ///#....................... % .........#
        ///#.###.#####.#########.########.####.#
        ///#.#.... * ........................#.#
        ///#.#.#### ########==######.#######...#
        ///#...####.#####.##==###### #######.#.#
        ///#.#............#........ * .......#.#
        ///#.####.#####.###.####.########.####.#
        ///#...... % .......#\.................#
        ///#####################################.
        /// </summary>
        internal static string level3 {
            get {
                return ResourceManager.GetString("level3", resourceCulture);
            }
        }
    }
}