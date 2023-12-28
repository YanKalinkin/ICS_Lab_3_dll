using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICS_Lab_3_dll
{
    public static class Cypher
    {
        public static string alphabet_h = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string alphabet_l = "abcdefghijklmnopqrstuvwxyz";
        public static string alphabet_s = "_1234567890";
        public static string alphabet_common = alphabet_h + alphabet_l + alphabet_s;
        public static string key_word = "";
        public static string common = "";
        public static int index;
        public static string space_replace(string find_space, bool mode)
        {
            string space_replaced = "";
            for (int i = 0; i < find_space.Length; i++)
            {
                if (mode)
                {
                    if (find_space[i] != ' ')
                        space_replaced += find_space[i];
                    else
                        space_replaced += '_';
                }
                else
                {
                    if (find_space[i] != '_')
                        space_replaced += find_space[i];
                    else
                        space_replaced += ' ';
                }

            }
            return space_replaced;
        }
        public static string Ceasar_with_key(string to_encrypt, int position, string key_word, bool mode)
        {
            string encrypted = "";
            string alph = alphabet_h + alphabet_l + alphabet_s;
            string alph_b = alph;
            string before_key = "";
            //Deleting key letters from alphabet
            for (int i = 0; i < alph.Length; i++)
            {
                for (int j = 0; j < key_word.Length; j++)
                {
                    if (alph[i] == key_word[j])
                    {
                        alph = alph.Remove(i, 1);
                    }
                }
            }
            //Seacring letters to set before key word (shift)
            for (int k = alph.Length - position; k < alph.Length; k++)
            {
                before_key += alph[k];
            }
            //Deleting shifted letters from alphabet
            for (int i = 0; i < alph.Length; i++)
            {
                for (int j = 0; j < before_key.Length; j++)
                {
                    if (alph[i] == before_key[j])
                    {
                        alph = alph.Remove(i, 1);
                    }
                }
            }
            //Collecting parts of the key string
            common = before_key + key_word + alph;
            //Now common is key, alph_b is alphabet
            if (mode)
            {
                for (int n = 0; n < to_encrypt.Length; n++)
                {
                    for (int m = 0; m < alph_b.Length; m++)
                    {
                        if (to_encrypt[n] == alph_b[m])
                        {
                            encrypted += common[m];
                        }
                    }
                }
            }
            else
            {
                for (int n = 0; n < to_encrypt.Length; n++)
                {
                    for (int m = 0; m < common.Length; m++)
                    {
                        if (to_encrypt[n] == common[m])
                        {
                            encrypted += alph_b[m];
                        }
                    }
                }
            }
            return encrypted;
        }
        public static string Vigenere(string to_encrypt, string slogan, bool mode)
        {
            int row_index = 0;
            int col_index = 0;
            string alph = alphabet_h + alphabet_l + alphabet_s;
            string[] vigenere_matrix = new string[alph.Length];
            string encrypted = "";
            string shift(string to_shift, int step)
            {
                string after = "";
                string before = "";
                for (int i = 0; i < step; i++)
                {
                    after += to_shift[i];
                }
                before = to_shift.Remove(0, step);
                return before + after;
            }
            //Matrix filling
            for (int i = 0; i < vigenere_matrix.Length; i++)
            {
                vigenere_matrix[i] += shift(alph, i);
            }
            //encrypting
            if (mode)
            {
                for (int l = 0; l < to_encrypt.Length; l++)
                {
                    for (int i = 0; i < vigenere_matrix.Length; i++)
                    {
                        if (vigenere_matrix[i][0] == slogan[l])
                        {
                            row_index = i;
                            break;
                        }
                    }
                    for (int j = 0; j < vigenere_matrix[0].Length; j++)
                    {
                        if (vigenere_matrix[0][j] == to_encrypt[l])
                        {
                            col_index = j;
                            break;
                        }
                    }
                    encrypted += vigenere_matrix[row_index][col_index];
                }
            }
            else
            {
                for (int l = 0; l < to_encrypt.Length; l++)
                {
                    for (int i = 0; i < vigenere_matrix.Length; i++)
                    {
                        if (vigenere_matrix[i][0] == slogan[l])
                        {
                            row_index = i;
                            break;
                        }
                    }
                    for (int j = 0; j < vigenere_matrix[row_index].Length; j++)
                    {
                        if (vigenere_matrix[row_index][j] == to_encrypt[l])
                        {
                            col_index = j;
                            break;
                        }
                    }
                    encrypted += vigenere_matrix[0][col_index];
                }
            }
            return encrypted;
        }
    }
}
